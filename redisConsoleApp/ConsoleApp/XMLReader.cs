using System;
using System.Xml;
using System.Collections;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections.Generic;
using System.IO;
using Serilog;

namespace ConsoleApp
{
    public class XMLReader
    {
        static public void ReadMicrodataFromDGT()
        {
            Log.Information("Searching for old data..");
            //Flush();
            const string kUrl = "https://dgt-microdata.s3.eu-central-1.amazonaws.com/";
            Log.Information($"Getting all XML keys  from {kUrl}...");
            List<string> keys = GetXMLKeys(kUrl);
            Log.Information("Done.");
            Log.Information($"Downloading files...");
            foreach (string item in keys)
                DownloadFile(kUrl + item);
        }

        static private List<string> GetXMLKeys(string XML)
        {
            const string kRegEx = ".*\\.zip$";
            List<string> keys = new List<string>();
            XmlTextReader reader = new XmlTextReader(XML);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                    if (reader.Name == "Key")
                    {
                        reader.Read();
                        Match match = Regex.Match(reader.Value, kRegEx, RegexOptions.IgnoreCase);
                        if (match.Success)
                            keys.Add(reader.Value);
                    }
            }
            return keys;
        }

        static private void DownloadFile(string zipUrl)
        {
            const string kZipPath = "C:\\RedisData\\temp.zip";
            const string kDataPath = "C:\\RedisData\\data";

            WebClient wb = new WebClient();
            if (!DataAlreadyExist(zipUrl, kDataPath))
            {
                wb.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.33 Safari/537.36");
                Log.Information("Downloading..");
                wb.DownloadFile(zipUrl, kZipPath);
                Log.Information("Extracting..");
                System.IO.Compression.ZipFile.ExtractToDirectory(kZipPath, kDataPath);
                Log.Information("Deleting zip file..");
                File.Delete(kZipPath);
                Log.Information("Done.");
            };
        }

        static private bool DataAlreadyExist(string zipUrl, string path)
        {
            if (!Directory.Exists(path))
            {
                Log.Information($"Directory {path} doesn't exist, creating it..");
                Directory.CreateDirectory(path);
                Log.Information("Done.");

            }
            string newFileName = $"{path}\\{zipUrl.Substring(57, 26).Trim()}txt";
            Log.Information($"File name: {newFileName}");

            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            
                if (newFileName == file)
                {
                    Log.Information("File already in the database.");
                    return true;
                }
            Log.Information("New file!");
            return false;
        }

        static private void Flush()
        {
            const string path = "C:\\RedisData\\data";
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
                Log.Information($"Deleted old file: {file}");
            }
        }
    }
}