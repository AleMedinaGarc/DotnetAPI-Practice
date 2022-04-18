using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APICarData.Domain.Models;
using APICarData.Domain.Data.Entities;

namespace APICarData.Services
{
    public class ReservationsService
    {
        private readonly Dal.ReservationsDAL _DAL;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservationsService(Dal.ReservationsDAL DAL, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _DAL = DAL;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<ReservationModel>> GetReservationsByUser()
        {
            try
            {
                var currentUser = GetCurrentUser();
                var reservations = await _DAL.GetReservationsByUser(currentUser.UserId);
                var reservationsMapped = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationModel>>(reservations);
                return reservationsMapped;
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public async Task<IEnumerable<ReservationModel>> GetAllReservations()
        {
            try
            {
                var reservations = await _DAL.GetAllReservations();
                var reservationsMapped = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationModel>>(reservations);
                return reservationsMapped;
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public void AddReservation(ReservationModel reservation)
        {
            try
            {
                var reservationMapped = _mapper.Map<ReservationModel, Reservation>(reservation);
                _DAL.AddReservation(reservationMapped);

            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public void UpdateReservationById(ReservationModel reservation, int id)
        {
            try
            {
                if (reservation.ReservationId == id)
                {
                    var reservationMapped = _mapper.Map<ReservationModel, Reservation>(reservation);
                    _DAL.UpdateReservation(reservationMapped);

                }

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
                var reservation = _DAL.GetReservationById(id);
                _DAL.DeleteReservation(reservation);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
        private User GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    Email = userClaims.FirstOrDefault(o =>
                        o.Type == ClaimTypes.Email)?.Value,
                    Role = userClaims.FirstOrDefault(o =>
                        o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
