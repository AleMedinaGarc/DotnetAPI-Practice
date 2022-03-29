using Microsoft.EntityFrameworkCore;
using APICarData.Data.Entities;

namespace APICarData.Data.CarsContext
{
    public class CarsContext : DbContext
    {
        public CarsContext(DbContextOptions<CarsContext> options):base(options)
        {
        }
        public DbSet<Car> Cars {get; set; } 
        //public DbSet<User> Users {get; set; }
    }
}