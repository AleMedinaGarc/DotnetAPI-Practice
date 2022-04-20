using APICarData.Domain.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APICarData.Domain.Interfaces.Reservations
{
    public interface IReservationsDAL
    {
        Task<IEnumerable<Reservation>> GetUserReservations(string userId);
        Reservation GetReservationById(int id);
        Task<IEnumerable<Reservation>> GetAllReservations();
        void AddReservation(Reservation reservation);
        void UpdateReservation(Reservation reservation, Reservation oldReservation);
        void DeleteReservationById(int id);
        bool ReservationExistById(int id);
        bool CarAlreadyTaken(string VIN);
        bool UserHasReservations(string userId);
        bool ReservationsEmpty();
        bool ReservationExist(int id);
    }
}
