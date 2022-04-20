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
       
        void Insert<T>(T value);
        void UpdateEntry<T>(T value);
        void Delete<T>(T value);
        void Detach<T>(T value);
    }
}
