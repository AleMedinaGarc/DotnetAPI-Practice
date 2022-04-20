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
        bool AddReservation(ReservationModel reservation);
        bool UpdateReservation(ReservationModel reservation);
        bool DeleteReservation(int id);
    }
}
