using System;
using System.ComponentModel.DataAnnotations;

namespace APICarData.Domain.Models
{ 
    public class UserModel : GoogleUserDataModel
    {
        [RegularExpression(@"^\+[0-9]{2} [0-9]{9}$")]
        public string PhoneNumber {get; set; }
        [StringLength(50)]
        public string Department {get; set; }
        [StringLength(100)]
        public string AddressFormatted {get; set; }
        [StringLength(13)]
        public string Role {get; set; } // administrator/employee
        [Required]
        public DateTime CreationDate{get; set; }
        [Required]
        public DateTime LastLogin {get; set; }
    }
}