using System.ComponentModel.DataAnnotations;

namespace APICarData.Data.Entities 
{ 
    public class User : GoogleUserData
    {
        [RegularExpression(@"^(?:{9}[0-9]$")]
        public string contactNumber {get; set; }
        [StringLength(70)]
        public string address {get; set; }
        [Required]
        [StringLength(13)]
        public string role {get; set; } = "employee";// administrator/employee
        [Required]
        public string creationDate{get; set; }
        [Required]
        public string lastLogin {get; set; }
    }
}