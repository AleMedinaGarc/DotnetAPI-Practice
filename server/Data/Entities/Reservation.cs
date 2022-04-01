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
        [RegularExpression(@"^(?:[0-3][0-9]-[0-1][0-9]-20{2}[0-9]$")]
        public int fromDate {get; set; }
        [Required]
        [RegularExpression(@"^(?:[0-3][0-9]-[0-1][0-9]-20{2}[0-9]$")]
        public string toDate {get; set; }
        [Required]
        [StringLength(7)] // personal/shared
        public string use {get; set; }
    }
}