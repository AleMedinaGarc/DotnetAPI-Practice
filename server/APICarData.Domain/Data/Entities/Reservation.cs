using System;
using System.ComponentModel.DataAnnotations;

namespace APICarData.Domain.Data.Entities 
{ 
    public class Reservation
    {
        [Key]
        public int ReservationId {get; set; }
        [Required]
        public string UserId {get; set; }
        [Required]
        public string VIN {get; set; }
        [Required]
        public string FromDate {get; set; }
        [Required]
        public string ToDate {get; set; }
        [Required]
        public string CarUse {get; set; }
    }
}