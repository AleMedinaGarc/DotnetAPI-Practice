using System.ComponentModel.DataAnnotations;

namespace APICarData.Domain.Data.Entities 
{ 
    public class CompanyCar
    {
        [Key]
        public string VIN { get; set; }
        [Required]
        public string numberPlate {get; set; }
        [Required]
        public int fabricationYear {get; set; }
        [Required]
        public string nextITV {get; set; }
        [Required]
        public string nextCarInspection { get; set; }
    }
}