using Newtonsoft.Json;
using ConsoleApp.Models;
using System;
using System.Xml;
using System.Collections;
using static ConsoleApp.XMLReader;
using static ConsoleApp.CarSerializer;
using Serilog;
using System.IO;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("./consoleapp.log")
                .CreateLogger();

            Console.WriteLine("Redis Console App");
            Console.WriteLine("----------------------");
            Console.WriteLine("This console application reads the DGT microdata at: https://dgt-microdata.s3.eu-central-1.amazonaws.com/ and populate a running Redis database in localhost.");
            Console.WriteLine("It filters the non valid VIN cars and non valid line sizes");

            int numberOfThreads = GetNumberOfThreads();

            Log.Information("Reading DGT microdata.");
            ReadMicrodataFromDGT();
            Log.Information("Adding DGT data to the database. This may take a long time.");
            InitAppThreaded(numberOfThreads);
            Log.Information("Finished!");


        }

        public static int GetNumberOfThreads()
        {
            int numberOfThreads = 10;
            
            Console.WriteLine("The Redis SET operation it's made with multithreading");
            Console.WriteLine("Due the Redis bottleneck, is configured to use 10 threads.");
            Console.WriteLine("Do you want to change the default configuration? (Y/N)");

            char option = Console.ReadLine()[0];
            while (option != 'Y' && option != 'N' && option != 'y' && option != 'n')
            {
                Console.WriteLine("Do you want to change the default configuration? (Y/N)");
                option = Console.ReadLine()[0];
            }

            if (option != 'N')
            {
                Console.WriteLine("Introduce a number of threads:");
                string threadsRaw = Console.ReadLine();
                while (int.TryParse(threadsRaw, out _) != true || int.Parse(threadsRaw) < 0 || int.Parse(threadsRaw) > 100)
                {
                    Console.WriteLine("Number not valid.");
                    Console.WriteLine("Introduce a number of threads:");
                    threadsRaw = Console.ReadLine();

                }
                numberOfThreads = int.Parse(threadsRaw);
            }
            return numberOfThreads;
        }

        public static void InitAppThreaded(int numberOfThreads)
        {
            const int kLineSize = 714;
            Thread[] threadsArray = new Thread[numberOfThreads];
            const string kDataPath = "C:\\RedisData\\data";
            string[] files = Directory.GetFiles(kDataPath);
            double[] lineCount = CountFileLines(files);
            int[] timeStamps = new int[files.Length];
            var connection = ConnectionHelper.Connection.GetDatabase();
            double totalCounter = 0;
            int addedCounter = 0;
            int checkedEntrys = 0;
            int currentFile = 0;

            foreach (string file in files)
            {
                Log.Information($"Adding {file} data.. ");
                for (int currentThread = 0; currentThread < numberOfThreads; currentThread++)
                {
                    var linesPerThread = Convert.ToInt32(Math.Floor(lineCount[currentFile] / numberOfThreads));
                    var localCurrentThread = currentThread;
                    threadsArray[localCurrentThread] = new Thread(() =>
                    {
                        var localCheckedEntrys = 0;
                        var startingLine = File.ReadLines(file).Skip((localCurrentThread * linesPerThread) - 1);
                        foreach (string line in startingLine)
                        {
                            Interlocked.Increment(ref checkedEntrys);
                            if (line.Length == kLineSize && SaveDataToRedis(DGTCarSample: MapCarInfo(line), connection))
                                Interlocked.Increment(ref addedCounter);
                            localCheckedEntrys++;

                            if ((localCheckedEntrys == linesPerThread && localCurrentThread != (numberOfThreads - 1)) ||
                                (localCurrentThread == numberOfThreads - 1 && line == null))
                                break;
                        }
                    });
                }
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                foreach (var thread in threadsArray)
                    thread.Start();

                foreach (var thread in threadsArray)
                    thread.Join();

                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                timeStamps[currentFile] = ts.Minutes;

                Log.Information($"Done. Time taken: {elapsedTime}");
                Log.Information($"File: {file}");
                Log.Information($"File entrys: {lineCount[currentFile]}. Checked: {checkedEntrys - 1}. Added: {addedCounter}.");
                totalCounter = +addedCounter;
                checkedEntrys = 0;
                addedCounter = 0;
                currentFile++;

            }
            double avg = Queryable.Average(timeStamps.AsQueryable());
            Console.WriteLine($"Total entrys added: {totalCounter}.");
            Console.WriteLine($"Total time: {timeStamps.Sum()}. Mean time: {totalCounter}.");
        }


        public static double[] CountFileLines(string[] files)
        {
            double[] lineCount = new double[files.Count()];
            int index = 0;

            Log.Information("Checking files length..");

            foreach (string file in files)
            {
                Log.Information($"Checking {file}..");
                StreamReader sr = new StreamReader(file);
                string value = sr.ReadLine();
                while (value != null)
                {
                    lineCount[index] = lineCount[index] + 1;
                    value = sr.ReadLine();
                }
                Log.Information($"Done. Entrys found: {lineCount[index]}");
                index++;
            }

            return lineCount;
        }

        public static bool SaveDataToRedis(DGTCar DGTCarSample, IDatabase connection)
        {
            string productCacheKey = DGTCarSample.VIN;
            if (productCacheKey.Length == 17)
            {
                var serializedObject = JsonConvert.SerializeObject(DGTCarSample);
                connection.StringSet(productCacheKey, serializedObject);
                return true;
            }
            return false;
        }
    }
}
