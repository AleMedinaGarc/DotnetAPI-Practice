using System.ComponentModel.DataAnnotations;

namespace APICarData.Data.Entities 
{ 
    public class Car
    {
        [Key]
        public int Id {get; set; }
        [Required]
        [StringLength(50)]
        public string Username {get; set; }
        [Required]
        [RegularExpression(@"^(?:[0-9]{4}[A-Z]{3})$")]
        public string PlateNumber {get; set; }
        [Required]
        [RegularExpression(@"^(?:[1-2][0-9]{3})$")]
        public int FabricationYear {get; set; }
        [Required]
        [RegularExpression(@"^(?:(1[0-2]|0[0-9])-2[0-9]{3})$")]
        public string NextITV {get; set; }
    }
}