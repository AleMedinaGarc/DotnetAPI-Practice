using System;
using System.ComponentModel.DataAnnotations;

namespace APICarData.Domain.Models
{ 
    public class UserModel : GoogleUserDataModel
    {
        [StringLength(15)]
        public string phoneNumber {get; set; }
        [StringLength(50)]
        public string department {get; set; }
        [StringLength(100)]
        public string addressFormatted {get; set; }
        [StringLength(13)]
        public string role {get; set; } // administrator/employee
        [Required]
        public DateTime creationDate{get; set; }
        [Required]
        public DateTime lastLogin {get; set; }
    }
}