using APICarData.Domain.Data;
using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces;
using APICarData.Domain.Interfaces.Reservations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace APICarData.Dal
{
    public class ReservationsDAL : IReservationsDAL
    {
        private readonly IApiContext _context;
        public ReservationsDAL(IApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetUserReservations(string userId)
        {
            try
            {
                return await _context.Reservations.Where(p =>
                    p.UserId == userId).ToListAsync();
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public Reservation GetReservationById(int id)
        {
            try
            {
                return _context.Reservations.FirstOrDefault(p => p.ReservationId == id);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            try
            {
                return await _context.Reservations.ToListAsync();
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public void AddReservation(Reservation reservation)
        {
            try
            {
                _context.Insert(reservation);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
        public void UpdateReservation(Reservation reservation, Reservation oldReservation)
        {
            try
            {
                _context.Detach(oldReservation); 
                _context.UpdateEntry(reservation);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public void DeleteReservationById(int id)
        {
            try
            {
                var reservation = _context.Reservations.FirstOrDefault(p => p.ReservationId == id);
                _context.Delete(reservation);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public bool ReservationExistById(int id)
        {
            try
            {
                return _context.Reservations.Any(p => p.ReservationId == id);
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }

        public bool CarAlreadyTaken(string VIN)
        {
            try
            {
                return _context.Reservations.Any(p => p.VIN == VIN);
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }

        public bool UserHasReservations(string userId)
        {
            try
            {
                return _context.Reservations.Any(p => p.UserId == userId);
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }

        public bool ReservationsEmpty()
        {
            try
            {
                return !_context.Reservations.Any();
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }

        public bool ReservationExist(int id)
        {
            try
            {
                return _context.Reservations.Any(p => p.ReservationId== id);
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }

        public bool CarExist(string VIN)
        {
            try
            {
                return _context.CompanyCars.Any(p => p.VIN == VIN);
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }
        public bool UserExist(string id)
        {
            try
            {
                return _context.Users.Any(p => p.UserId == id);
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }

    }

}

