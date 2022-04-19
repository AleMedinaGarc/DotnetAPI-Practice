
using Newtonsoft.Json;
using ConsoleApp.Models;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            foreach (string line in System.IO.File.ReadLines(@"C:\Users\alme\Documents\ConsoleApp\ConsoleApp\rawData\export_mensual_mat_202112.txt"))
            {
                SaveDataToRedis(DGTCarSample: MapCarInfo(line));
            }
        }

        public static void SaveDataToRedis(DGTCar DGTCarSample)
        {
            string productCacheKey = DGTCarSample.VIN;
            var connection = RedisHelper.Connection.GetDatabase();
            var serializedObject = JsonConvert.SerializeObject(DGTCarSample);
            Console.WriteLine(serializedObject);
            connection.StringSet(productCacheKey, serializedObject);

        }

        public static DGTCar MapCarInfo(string line)
        {
            DGTCar DGTCarSample = new DGTCar();

            DGTCarSample.RegistrationDate = DateFormatDGT(line.Substring(0, 8).Trim());
            DGTCarSample.RegistrationClass = line.Substring(8, 1).Trim();
            DGTCarSample.ProcessingDate = DateFormatDGT(line.Substring(9, 8).Trim());
            DGTCarSample.Brand = line.Substring(17, 30).Trim();
            DGTCarSample.Model = line.Substring(47, 22).Trim();
            DGTCarSample.OriginClass = line.Substring(69, 1).Trim();
            DGTCarSample.VIN = line.Substring(70, 21).Trim();
            DGTCarSample.VehicleType = line.Substring(91, 2).Trim();
            DGTCarSample.FuelType = line.Substring(93, 1).Trim();
            DGTCarSample.CC = ParseInt(line.Substring(94, 5).Trim());
            DGTCarSample.TaxableHorsePower = ParseDouble(line.Substring(99, 6).Trim());
            DGTCarSample.WeightIndicator = ParseDouble(line.Substring(105, 6).Trim());
            DGTCarSample.GrossWeight = ParseDouble(line.Substring(111, 6).Trim());
            DGTCarSample.Seats = ParseInt(line.Substring(117, 3).Trim());
            DGTCarSample.IsSealed = line.Substring(120, 2).Trim();
            DGTCarSample.IsSeized = line.Substring(122, 2).Trim();
            DGTCarSample.Transmissions = ParseInt(line.Substring(124, 2).Trim());
            DGTCarSample.Owners = ParseInt(line.Substring(126, 2).Trim());
            DGTCarSample.Location = line.Substring(128, 24).Trim();
            DGTCarSample.GeoLocationCode = line.Substring(152, 2).Trim();
            DGTCarSample.GeoRegistrationCode = line.Substring(154, 2).Trim();
            DGTCarSample.RegistrationType = line.Substring(156, 1).Trim();
            DGTCarSample.RegistrationTypeDate = DateFormatDGT(line.Substring(157, 8).Trim());
            DGTCarSample.PostalCode = ParseInt(line.Substring(165, 5).Trim());
            DGTCarSample.FirstRegistrationDate = DateFormatDGT(line.Substring(170, 8).Trim());
            DGTCarSample.IsNew = line.Substring(178, 1).Trim();
            DGTCarSample.OwnerType = line.Substring(179, 1).Trim();
            DGTCarSample.ITVCode = line.Substring(180, 9).Trim();
            DGTCarSample.ServiceCode = line.Substring(189, 3).Trim();
            DGTCarSample.INECode = ParseInt(line.Substring(192, 5).Trim());
            DGTCarSample.Municipality = line.Substring(197, 30).Trim();
            DGTCarSample.HorsePower = ParseDouble(line.Substring(227, 7).Trim());
            DGTCarSample.MaxSeats = ParseInt(line.Substring(234, 3).Trim());
            DGTCarSample.CO2Emissions = ParseInt(line.Substring(237, 5).Trim());
            DGTCarSample.IsRental = line.Substring(242, 1).Trim();
            DGTCarSample.OwnerHasTitle = line.Substring(243, 1).Trim();
            DGTCarSample.OwnershipTypeCode = line.Substring(244, 1).Trim();
            DGTCarSample.PermanentlyUnregistered = line.Substring(245, 1).Trim();
            DGTCarSample.IsTemporarilyDeregistered = line.Substring(246, 1).Trim();
            DGTCarSample.IsStolen = line.Substring(247, 1).Trim();
            DGTCarSample.IsGPSGPRSUnregistered = line.Substring(248, 11).Trim();
            DGTCarSample.ITVType = line.Substring(259, 25).Trim();
            DGTCarSample.VehicleVariant = line.Substring(284, 25).Trim();
            DGTCarSample.VehicleVersion = line.Substring(309, 35).Trim();
            DGTCarSample.Manufacturer = line.Substring(344, 70).Trim();
            DGTCarSample.CurbWeight = ParseInt(line.Substring(414, 6).Trim());
            DGTCarSample.MaxCurbWeight = ParseInt(line.Substring(420, 6).Trim());
            DGTCarSample.ProductionModelCode = line.Substring(426, 4).Trim();
            DGTCarSample.BodyStyleCode = line.Substring(430, 4).Trim();
            DGTCarSample.PassengersPerSquareMeter = ParseInt(line.Substring(434, 3).Trim());
            DGTCarSample.EuroEmissionLevel = line.Substring(437, 8).Trim();
            DGTCarSample.ElectricPowerConsumption = ParseInt(line.Substring(445, 4).Trim());
            DGTCarSample.VehicleRegulationClass = line.Substring(449, 4).Trim();
            DGTCarSample.ElectricVehicleCategory = line.Substring(453, 4).Trim();
            DGTCarSample.ElectricVehicleAutonomy = ParseInt(line.Substring(457, 6).Trim());
            DGTCarSample.ChassisBrand = line.Substring(463, 30).Trim();
            DGTCarSample.ChassisManufacturer = line.Substring(493, 50).Trim();
            DGTCarSample.ChassisVehicleType = line.Substring(543, 35).Trim();
            DGTCarSample.ChassisVehicleVariant = line.Substring(578, 25).Trim();
            DGTCarSample.ChassisVehicleBase = line.Substring(603, 35).Trim(); //added
            DGTCarSample.Wheelbase = ParseInt(line.Substring(638, 4).Trim());
            DGTCarSample.FrontAxleTrack = ParseInt(line.Substring(642, 4).Trim());
            DGTCarSample.RearAxleTrack = ParseInt(line.Substring(646, 4).Trim());
            DGTCarSample.EngineIntakeType = line.Substring(650, 1).Trim();
            DGTCarSample.ProductionModelPassword = line.Substring(651, 25).Trim();
            DGTCarSample.EcoInnovation = line.Substring(676, 1).Trim();
            DGTCarSample.EcoReduction = line.Substring(677, 4).Trim();
            DGTCarSample.EcoCode = line.Substring(681, 25).Trim();
            DGTCarSample.EnrollmentDate = DateFormatDGT(line.Substring(706, 8).Trim());

            return DGTCarSample;
        }

        public static string DateFormatDGT(string date)
        {
            return date == "" ? "" : date.Insert(2, "/").Insert(5, "/");
        }

        public static int ParseInt(string numberS)
        {
            int _o1;
            const int _kEmptyInt = 0;

            return int.TryParse(numberS, out _o1) ? int.Parse(numberS): _kEmptyInt;
        }
        public static double ParseDouble(string numberS)
        {
            double _o2;
            const double _kEmptyDouble = 0;

            return double.TryParse(numberS, out _o2) ? double.Parse(numberS) : _kEmptyDouble;
        }
    }
}
