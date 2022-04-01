using System.ComponentModel.DataAnnotations;

namespace APICarData.Data.Entities 
{ 
    public class CompanyCar : DGTCar
    {
        [Required]
        [StringLength(50)]
        public string username {get; set; }
        [Required]
        [RegularExpression(@"^(?:[0-9]{4}[A-Z]{3})$")]
        public string numberPlate {get; set; }
        [Required]
        [RegularExpression(@"^(?:[1-2][0-9]{3})$")]
        public int fabricationYear {get; set; }
        [Required]
        [RegularExpression(@"^(?:(1[0-2]|0[0-9])-2[0-9]{3})$")]
        public string nextITV {get; set; }
        [Required]
        [RegularExpression(@"^(?:(1[0-2]|0[0-9])-2[0-9]{3})$")]
        public string nexTInspection {get; set; }
    }
}