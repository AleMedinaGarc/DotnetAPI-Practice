using System.ComponentModel.DataAnnotations;

namespace APICarData.Domain.Models 
{ 
    public class CompanyCarModel
    {
        [Key]
        public string VIN { get; set; }
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
        public string nextCarInspection { get; set; }
    }
}