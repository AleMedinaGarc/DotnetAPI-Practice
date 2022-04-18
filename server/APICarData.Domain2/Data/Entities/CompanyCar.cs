using System.ComponentModel.DataAnnotations;

namespace APICarData.Domain.Data.Entities 
{ 
    public class CompanyCar
    {
        [Key]
        public string VIN { get; set; }
        [Required]
        public string NumberPlate {get; set; }
        [Required]
        public int FabricationYear {get; set; }
        [Required]
        public string NextITV {get; set; }
        [Required]
        public string NextCarInspection { get; set; }

    }
}