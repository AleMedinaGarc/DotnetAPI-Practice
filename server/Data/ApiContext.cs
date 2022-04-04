using Microsoft.EntityFrameworkCore;
using APICarData.Data.Entities;

namespace APICarData.Data.ApiContext
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options):base(options)
        {
        }
        public DbSet<CompanyCar> CompanyCars {get; set; } 
        public DbSet<User> Users {get; set; }
        public DbSet<GoogleUserData> GoogleUsersData {get; set; }
        public DbSet<Reservation> Reservations {get; set; }
    }
}