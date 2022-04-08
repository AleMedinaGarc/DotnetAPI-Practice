using System.ComponentModel.DataAnnotations;

namespace APICarData.BusinessLogicLayer.Models
{
    public class DGTCarModel
    {
        [Key]
        public string VIN { get; set; }
        public string RegistrationDate { get; set; }
        public string RegistrationClass { get; set; }
        public string ProcessingDate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string OriginClass { get; set; }
        public string VehicleType { get; set; }
        public string FuelType { get; set; }
        public int CC { get; set; } 
        public double TaxableHorsePower { get; set; }
        public double WeightIndicator { get; set; }
        public double GrossWeight { get; set; }
        public int Seats { get; set; }
        public string IsSealed { get; set; }
        public string IsSeized { get; set; }
        public int Transmissions { get; set; }
        public int Owners { get; set; }
        public string Location { get; set; }
        public string GeoLocationCode { get; set; }
        public string GeoRegistrationCode { get; set; }
        public string RegistrationType { get; set; }
        public string RegistrationTypeDate { get; set; }
        public int PostalCode { get; set; }
        public string FirstRegistrationDate { get; set; }
        public string IsNew { get; set; }
        public string OwnerType { get; set; }
        public string ITVCode { get; set; }
        public string ServiceCode { get; set; }
        public int INECode { get; set; }
        public string Municipality { get; set; }
        public double HorsePower { get; set; }
        public int MaxSeats { get; set; }
        public int CO2Emissions { get; set; }
        public string IsRental { get; set; }
        public string OwnerHasTitle { get; set; }
        public string OwnershipTypeCode { get; set; }
        public string PermanentlyUnregistered { get; set; }
        public string IsTemporarilyDeregistered { get; set; }
        public string IsStolen { get; set; }
        public string IsGPSGPRSUnregistered { get; set; }
        public string ITVType { get; set; }
        public string VehicleVariant { get; set; }
        public string VehicleVersion { get; set; }
        public string Manufacturer { get; set; }
        public int CurbWeight { get; set; }
        public int MaxCurbWeight { get; set; }
        public string ProductionModelCode { get; set; }
        public string BodyStyleCode { get; set; }
        public int PassengersPerSquareMeter { get; set; }
        public string EuroEmissionLevel { get; set; }
        public int ElectricPowerConsumption { get; set; }
        public string VehicleRegulationClass { get; set; }
        public string ElectricVehicleCategory { get; set; }
        public int ElectricVehicleAutonomy { get; set; }
        public string ChassisBrand { get; set; }
        public string ChassisManufacturer { get; set; }
        public string ChassisVehicleType { get; set; }
        public string ChassisVehicleVariant { get; set; }
        public string ChassisVehicleBase { get; set; }
        public int Wheelbase { get; set; }
        public int FrontAxleTrack { get; set; }
        public int RearAxleTrack { get; set; }
        public string EngineIntakeType { get; set; }
        public string ProductionModelPassword { get; set; }
        public string EcoInnovation { get; set; }
        public string EcoReduction { get; set; }
        public string EcoCode { get; set; }
        public string EnrollmentDate { get; set; }
    }
}
