using System.ComponentModel.DataAnnotations;

namespace APICarData.Data.Entities 
{ 
    public class Car
    {
        [Key]
        public int Id {get; set; }
        [Required]
        public string User {get; set; }
        [Required]
        public string PlateNumber {get; set; }
        [Required]
        public int FabricationYear {get; set; }
        [Required]
        public string NextITV {get; set; }
    }
}