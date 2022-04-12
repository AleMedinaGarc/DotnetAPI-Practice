using Microsoft.EntityFrameworkCore;
using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces;
using System.Threading.Tasks;

namespace APICarData.Domain.Data
{
    public class ApiContext : DbContext, IApiContext
    {
        public ApiContext(DbContextOptions<ApiContext> options):base(options)
        {
        }
        public DbSet<CompanyCar> CompanyCars {get; set; } 
        public DbSet<User> Users {get; set; }
        public DbSet<Reservation> Reservations {get; set; }

        public void Insert(User user)
        {
            Users.Add(user);
            SaveChanges();
        }
    }
}