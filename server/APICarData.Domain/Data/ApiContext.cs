using Microsoft.EntityFrameworkCore;
using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces;
using System;

namespace APICarData.Domain.Data
{
    public class ApiContext : DbContext, IApiContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }
        public DbSet<CompanyCar> CompanyCars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


        public void Insert<T>(T value)
        {
            try
            {
                switch (value.GetType().Name)
                {
                    case "CompanyCar":
                        CompanyCars.Add(value as CompanyCar);
                        break;
                    case "User":
                        Users.Add(value as User);
                        break;
                    case "Reservation":
                        Reservations.Add(value as Reservation);
                        break;
                }
                SaveChanges();
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public void UpdateEntry<T>(T value)
        {
            try
            {
                Entry(value).State = EntityState.Modified;
                SaveChanges();
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
        public void Delete<T>(T value)
        {
            try
            {
                switch (value.GetType().Name)
                {
                    case "CompanyCar":
                        CompanyCars.Remove(value as CompanyCar);
                        break;
                    case "User":
                        Users.Remove(value as User);
                        break;
                    case "Reservation":
                        Reservations.Remove(value as Reservation);
                        break;
                }
                SaveChanges();
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public void Detach<T>(T value)
        {
            Entry(value).State = EntityState.Detached;
        }
    }
}