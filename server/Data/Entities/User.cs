using System.ComponentModel.DataAnnotations;

namespace APICarData.Data.Entities 
{ 
    public class User : GoogleUserData
    {
        [Required]
        [RegularExpression(@"^(?:{9}[0-9]$")]
        public string contactNumber {get; set; }
        [Required]
        [StringLength(70)]
        public string address {get; set; }
        [Required]
        [StringLength(13)]
        public string role {get; set; } // administrator/employee
    }
}