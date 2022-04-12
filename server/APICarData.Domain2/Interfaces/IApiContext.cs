using APICarData.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APICarData.Domain.Interfaces
{
    public interface IApiContext
    {
        public DbSet<CompanyCar> CompanyCars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public void Insert(User user);
    }
}
