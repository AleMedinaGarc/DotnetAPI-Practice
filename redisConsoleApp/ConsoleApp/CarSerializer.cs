using ConsoleApp.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public class CarSerializer
    {
        public static DGTCar MapCarInfo(string line)
        {

            DGTCar DGTCarSample = new DGTCar
            {
                RegistrationDate = DateFormatDGT(line[..8].Trim()),
                RegistrationClass = line.Substring(8, 1).Trim(),
                ProcessingDate = DateFormatDGT(line.Substring(9, 8).Trim()),
                Brand = line.Substring(17, 30).Trim(),
                Model = line.Substring(47, 22).Trim(),
                OriginClass = line.Substring(69, 1).Trim(),
                VIN = line.Substring(70, 21).Trim(),
                VehicleType = line.Substring(91, 2).Trim(),
                FuelType = line.Substring(93, 1).Trim(),
                CC = ParseInt(line.Substring(94, 5).Trim()),
                TaxableHorsePower = ParseDouble(line.Substring(99, 6).Trim()),
                WeightIndicator = ParseDouble(line.Substring(105, 6).Trim()),
                GrossWeight = ParseDouble(line.Substring(111, 6).Trim()),
                Seats = ParseInt(line.Substring(117, 3).Trim()),
                IsSealed = line.Substring(120, 2).Trim(),
                IsSeized = line.Substring(122, 2).Trim(),
                Transmissions = ParseInt(line.Substring(124, 2).Trim()),
                Owners = ParseInt(line.Substring(126, 2).Trim()),
                Location = line.Substring(128, 24).Trim(),
                GeoLocationCode = line.Substring(152, 2).Trim(),
                GeoRegistrationCode = line.Substring(154, 2).Trim(),
                RegistrationType = line.Substring(156, 1).Trim(),
                RegistrationTypeDate = DateFormatDGT(line.Substring(157, 8).Trim()),
                PostalCode = ParseInt(line.Substring(165, 5).Trim()),
                FirstRegistrationDate = DateFormatDGT(line.Substring(170, 8).Trim()),
                IsNew = line.Substring(178, 1).Trim(),
                OwnerType = line.Substring(179, 1).Trim(),
                ITVCode = line.Substring(180, 9).Trim(),
                ServiceCode = line.Substring(189, 3).Trim(),
                INECode = ParseInt(line.Substring(192, 5).Trim()),
                Municipality = line.Substring(197, 30).Trim(),
                HorsePower = ParseDouble(line.Substring(227, 7).Trim()),
                MaxSeats = ParseInt(line.Substring(234, 3).Trim()),
                CO2Emissions = ParseInt(line.Substring(237, 5).Trim()),
                IsRental = line.Substring(242, 1).Trim(),
                OwnerHasTitle = line.Substring(243, 1).Trim(),
                OwnershipTypeCode = line.Substring(244, 1).Trim(),
                PermanentlyUnregistered = line.Substring(245, 1).Trim(),
                IsTemporarilyDeregistered = line.Substring(246, 1).Trim(),
                IsStolen = line.Substring(247, 1).Trim(),
                IsGPSGPRSUnregistered = line.Substring(248, 11).Trim(),
                ITVType = line.Substring(259, 25).Trim(),
                VehicleVariant = line.Substring(284, 25).Trim(),
                VehicleVersion = line.Substring(309, 35).Trim(),
                Manufacturer = line.Substring(344, 70).Trim(),
                CurbWeight = ParseInt(line.Substring(414, 6).Trim()),
                MaxCurbWeight = ParseInt(line.Substring(420, 6).Trim()),
                ProductionModelCode = line.Substring(426, 4).Trim(),
                BodyStyleCode = line.Substring(430, 4).Trim(),
                PassengersPerSquareMeter = ParseInt(line.Substring(434, 3).Trim()),
                EuroEmissionLevel = line.Substring(437, 8).Trim(),
                ElectricPowerConsumption = ParseInt(line.Substring(445, 4).Trim()),
                VehicleRegulationClass = line.Substring(449, 4).Trim(),
                ElectricVehicleCategory = line.Substring(453, 4).Trim(),
                ElectricVehicleAutonomy = ParseInt(line.Substring(457, 6).Trim()),
                ChassisBrand = line.Substring(463, 30).Trim(),
                ChassisManufacturer = line.Substring(493, 50).Trim(),
                ChassisVehicleType = line.Substring(543, 35).Trim(),
                ChassisVehicleVariant = line.Substring(578, 25).Trim(),
                ChassisVehicleBase = line.Substring(603, 35).Trim(),
                Wheelbase = ParseInt(line.Substring(638, 4).Trim()),
                FrontAxleTrack = ParseInt(line.Substring(642, 4).Trim()),
                RearAxleTrack = ParseInt(line.Substring(646, 4).Trim()),
                EngineIntakeType = line.Substring(650, 1).Trim(),
                ProductionModelPassword = line.Substring(651, 25).Trim(),
                EcoInnovation = line.Substring(676, 1).Trim(),
                EcoReduction = line.Substring(677, 4).Trim(),
                EcoCode = line.Substring(681, 25).Trim(),
                EnrollmentDate = DateFormatDGT(line.Substring(706, 8).Trim())
            };
            return DGTCarSample;

        }

        public static string DateFormatDGT(string date)
        {
            try
            {
                return date == "" ? "" : date.Insert(2, "/").Insert(5, "/");
            }
            catch (Exception ex)
            {
                Log.Error($"Error while formating date: {date}. Details: {ex}");
                throw;
            }
        }

        public static int ParseInt(string numberS)
        {
            try
            {
                const int kEmptyInt = 0;
                return int.TryParse(numberS, out _) ? int.Parse(numberS) : kEmptyInt;
            }
            catch (Exception ex)
            {
                Log.Error($"Error while parsing to integer: {numberS}. Details: {ex}");
                throw;
            }
        }
        public static double ParseDouble(string numberS)
        {
            try
            {
                const double kEmptyDouble = 0;
                return double.TryParse(numberS, out _) ? double.Parse(numberS) : kEmptyDouble;
            }
            catch (Exception ex)
            {
                Log.Error($"Error while parsing to double: {numberS}. Details: {ex}");
                throw;
            }
        }
    }
}
