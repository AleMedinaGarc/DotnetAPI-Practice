using System;
using System.ComponentModel.DataAnnotations;

namespace APICarData.Domain.Data.Entities 
{ 
    public class User : GoogleUserData
    {
        [StringLength(15)]
        public string PhoneNumber {get; set; }
        [StringLength(50)]
        public string Department {get; set; }
        [StringLength(100)]
        public string AddressFormatted {get; set; }
        [Required]
        [StringLength(13)]
        public string Role {get; set; } // administrator/employee
        [Required]
        public DateTime CreationDate{get; set; }
        [Required]
        public DateTime LastLogin {get; set; }
    }
}