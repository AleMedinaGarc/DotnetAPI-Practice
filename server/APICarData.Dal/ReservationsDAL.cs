using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces;
using APICarData.Domain.Interfaces.Reservations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return await _context.Reservations.Where(p =>
                p.UserId == userId).ToListAsync();
        }

        public Reservation GetReservationById(int id)
        {
            return _context.Reservations.FirstOrDefault(p => p.ReservationId == id);
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _context.Reservations.ToListAsync();
        }

        public void AddReservation(Reservation reservation)
        {
            _context.Insert(reservation);
        }
        public void UpdateReservation(Reservation reservation, Reservation oldReservation)
        {
            _context.Detach(oldReservation);
            _context.UpdateEntry(reservation);
        }

        public void DeleteReservationById(int id)
        {
            var reservation = _context.Reservations.FirstOrDefault(p => p.ReservationId == id);
            _context.Delete(reservation);
        }

        public bool ReservationExistById(int id)
        {
            return _context.Reservations.Any(p => p.ReservationId == id);
        }

        public bool CarAlreadyTaken(string VIN)
        {
            return _context.Reservations.Any(p => p.VIN == VIN);
        }

        public bool UserHasReservations(string userId)
        {
            return _context.Reservations.Any(p => p.UserId == userId);
        }

        public bool ReservationsEmpty()
        {
            return !_context.Reservations.Any();
        }

        public bool ReservationExist(int id)
        {
            return _context.Reservations.Any(p => p.ReservationId == id);
        }

        public bool CarExist(string VIN)
        {
            return _context.CompanyCars.Any(p => p.VIN == VIN);
        }
        public bool UserExist(string id)
        {
            return _context.Users.Any(p => p.UserId == id);
        }

    }

}

