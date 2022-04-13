using APICarData.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APICarData.Domain.Interfaces
{
    public interface IApiContext
    {
        DbSet<CompanyCar> CompanyCars { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        //Login
        void InsertUser(User user);
        void UpdateUser(User user);
        // CompanyCars
        void InsertCar(CompanyCar car);
        void UpdateCar(CompanyCar car);
        void DeleteCar(CompanyCar car);
    }
}
