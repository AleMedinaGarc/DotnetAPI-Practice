using System.ComponentModel.DataAnnotations;

namespace APICarData.Domain.Models
{ 
    public class GoogleUserDataModel
    {
        [Key]
        public string UserId {get; set; }
        [Required]
        [StringLength(70)]
        public string FullName {get; set; }
        [Required]
        [StringLength(50)]
        public string GivenName {get; set; }
        [Required]
        [StringLength(20)]
        public string FamilyName {get; set; }
        [Required]
        public string ImageURL {get; set; }
        [Required]
        public string Email {get; set; }
    }
}