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
        Task<IEnumerable<ReservationModel>> GetAllReservations();
        void AddReservation(ReservationModel reservation);
        void UpdateReservationById(ReservationModel reservation, int id);
        void DeleteReservationById(int id);
    }
}
