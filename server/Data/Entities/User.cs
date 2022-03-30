using System.ComponentModel.DataAnnotations;

namespace APICarData.Data.Entities 
{ 
    public class User
    {
        [Key]
        public int Id {get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        public string Role { get; set; } = "GeneralUser";
    }
}