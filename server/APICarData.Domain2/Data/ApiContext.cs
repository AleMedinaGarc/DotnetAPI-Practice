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

        //Login
        public void InsertUser(User user)
        {
            Users.Add(user);
            SaveChanges();
        }
        public void UpdateUser(User user)
        {
            Entry(user).State = EntityState.Modified;
            SaveChanges();
        }
        public void DeleteUser(User user)
        {
            Entry(user).State = EntityState.Modified;
            SaveChanges();
        }

        //Cars
        public void InsertCar(CompanyCar car)
        {
            CompanyCars.Add(car);
            SaveChanges();
        }
 
        public void UpdateCar(CompanyCar car)
        {
            Entry(car).State = EntityState.Modified;
            SaveChanges();
        }
        public void DeleteCar(CompanyCar car)
        {
            CompanyCars.Remove(car);
            SaveChanges();
        }

    }
}