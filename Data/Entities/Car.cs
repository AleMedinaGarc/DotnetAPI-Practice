using System.ComponentModel.DataAnnotations;

namespace APICarData.Data.Entities 
{ 
    public class Car
    {
        [Key]
        public int Id {get; set; }
        public string User {get; set; }
        public string PlateNumber {get; set; }
        public int FabricationYear {get; set; }
        public string NextITV {get; set; }
    }
}