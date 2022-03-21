using Microsoft.EntityFrameworkCore;
using APICarData.Data.Entities;

namespace APICarData.Data.CarContex
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options):base(options)
        {
        }
        public DbSet<Car> Cars {get; set; } 
        // public DbSet<User> Users {get; set; }
    }
}