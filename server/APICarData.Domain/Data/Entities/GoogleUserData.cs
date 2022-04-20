using System.ComponentModel.DataAnnotations;

namespace APICarData.Domain.Data.Entities 
{ 
    public class GoogleUserData
    {
        [Key]
        public string UserId {get; set; }
        [Required]
        public string FullName {get; set; }
        [Required]
        public string GivenName {get; set; }
        [Required]
        public string FamilyName {get; set; }
        [Required]
        public string ImageURL {get; set; }
        [Required]
        public string Email {get; set; }
    }
}