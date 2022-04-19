using System;
using System.ComponentModel.DataAnnotations;

namespace APICarData.Domain.Models
{ 
    public class ReservationModel
    {
        [Key]
        public int ReservationId {get; set; }
        [Required]
        public int UserId {get; set; }
        [Required]
        [StringLength(21)]
        public string VIN {get; set; }
        [Required]
        [RegularExpression(@"^[0-3][0-9]-(1[0-2]|0[0-9])-2[0-9]{3}$")]
        public string FromDate {get; set; }
        [Required]
        [RegularExpression(@"^[0-3][0-9]-(1[0-2]|0[0-9])-2[0-9]{3}$")]
        public string ToDate {get; set; }
        [Required]
        [StringLength(8)] // personal/shared
        public string CarUse {get; set; }
    }
}