using APICarData.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APICarData.Domain.Interfaces.Reservations
{
    public interface IReservationsService        
    {
        Task<IEnumerable<ReservationModel>> GetCurrentUserReservations();
        Task<IEnumerable<ReservationModel>> GetUserReservations(string userId);
        Task<IEnumerable<ReservationModel>> GetAllReservations();
        void AddReservation(ReservationModel reservation);
        void UpdateReservation(ReservationModel reservation);
        void DeleteReservation(int id);
    }
}
