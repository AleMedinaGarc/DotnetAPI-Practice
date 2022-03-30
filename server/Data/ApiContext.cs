using Microsoft.EntityFrameworkCore;
using APICarData.Data.Entities;

namespace APICarData.Data.ApiContext
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options):base(options)
        {
        }
        public DbSet<Car> Cars {get; set; } 
        public DbSet<User> Users {get; set; }
    }
}