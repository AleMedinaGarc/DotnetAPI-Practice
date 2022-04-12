using APICarData.Domain.Data;
using APICarData.Domain.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APICarData.Dal
{
    public class ReservationsDAL
    {
        private readonly ApiContext _context;
        public ReservationsDAL(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUser(int userId)
        {
            return await _context.Reservations.Where(p =>
                p.userId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _context.Reservations.ToListAsync();
        }

        public void AddReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }
        public void UpdateReservation(Reservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteReservation(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();

        }

        public Reservation GetReservationById (int id)
        {
            return _context.Reservations.FirstOrDefault(p => p.vin == id);
        }
    }

}

