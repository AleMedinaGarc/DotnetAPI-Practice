using System;
using System.ComponentModel.DataAnnotations;

namespace APICarData.Domain.Data.Entities 
{ 
    public class User : GoogleUserData
    {
        [StringLength(15)]
        public string phoneNumber {get; set; }
        [StringLength(50)]
        public string department {get; set; }
        [StringLength(100)]
        public string addressFormatted {get; set; }
        [Required]
        [StringLength(13)]
        public string role {get; set; } // administrator/employee
        [Required]
        public DateTime creationDate{get; set; }
        [Required]
        public DateTime lastLogin {get; set; }
    }
}