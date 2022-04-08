using Microsoft.EntityFrameworkCore;
using APICarData.DataAccessLayer.Data.Entities;

namespace APICarData.DataAccessLayer.Data.ApiContext
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options):base(options)
        {
        }
        public DbSet<CompanyCar> CompanyCars {get; set; } 
        public DbSet<User> Users {get; set; }
        public DbSet<Reservation> Reservations {get; set; }
    }
}