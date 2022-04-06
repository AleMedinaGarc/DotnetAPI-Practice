using System;
using System.ComponentModel.DataAnnotations;

namespace APICarData.Data.Entities 
{ 
    public class Reservation
    {
        [Key]
        public int reservationId {get; set; }
        [Required]
        public int userId {get; set; }
        [Required]
        public int vin {get; set; }
        [Required]
        public DateTime fromDate {get; set; }
        [Required]
        public DateTime toDate {get; set; }
        [Required]
        [StringLength(7)] // personal/shared
        public string carUse {get; set; }
    }
}