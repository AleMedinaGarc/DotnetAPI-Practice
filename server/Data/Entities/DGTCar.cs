using System.ComponentModel.DataAnnotations;

namespace APICarData.Data.Entities 
{ 
    public class DGTCar
    {
        [Key]
        public int vin {get; set; }
    }
}