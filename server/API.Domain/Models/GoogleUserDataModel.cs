using System.ComponentModel.DataAnnotations;

namespace APICarData.Domain.Models
{ 
    public class GoogleUserDataModel
    {
        [Key]
        public int userId {get; set; }
        [Required]
        [StringLength(70)]
        public string fullName {get; set; }
        [Required]
        [StringLength(50)]
        public string givenName {get; set; }
        [Required]
        [StringLength(20)]

        public int familyName {get; set; }
        [Required]
        public string imageURL {get; set; }
        [Required]
        public string email {get; set; }
    }
}