using System.ComponentModel.DataAnnotations;

namespace APICarData.DataAccessLayer.Data.Entities 
{ 
    public class CompanyCar : DGTCar
    {
        [Required]
        [StringLength(50)]
        public string username {get; set; }
        [Required]
        public string numberPlate {get; set; }
        [Required]
        public int fabricationYear {get; set; }
        [Required]
        public string nextITV {get; set; }
        [Required]
        public string nexTInspection {get; set; }
    }
}