using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using APICarData.Domain.Models;
using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces.Reservations;

namespace APICarData.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly IReservationsDAL _DAL;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservationsService(IReservationsDAL DAL, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _DAL = DAL;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<ReservationModel>> GetCurrentUserReservations()
        {
            
            var currentUser = GetCurrentUser();
            // get the UserId
            // check if user has reservations
            var reservations = await _DAL.GetUserReservations(currentUser.UserId);
            var reservationsMapped = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationModel>>(reservations);
            return reservationsMapped;
        }

        public async Task<IEnumerable<ReservationModel>> GetAllReservations()
        {
            if (_DAL.ReservationsIsNotEmpty())
            {
                var reservations = await _DAL.GetAllReservations();
                var reservationsMapped = _mapper.Map<IEnumerable<Reservation>, IEnumerable<ReservationModel>>(reservations);
                return reservationsMapped;
            }
            else { 
                return Enumerable.Empty<ReservationModel>();
            }
        }

        public void AddReservation(ReservationModel reservation)
        {
            // check if body user exist
            // check if body car exist
            // check if body userId is (the same as the loged user) || admin
            if (!_DAL.CarAlreadyTaken(reservation.VIN))
            {
                var reservationMapped = _mapper.Map<ReservationModel, Reservation>(reservation);
                _DAL.AddReservation(reservationMapped);
            }
        }

        public void UpdateReservationById(ReservationModel reservation, int id)
        {
            if (reservation.ReservationId == id)
            {
                if (_DAL.ReservationExist(id))
                {
                // check if user id is (the same as the loged user) || admin
                // check if body user exist
                var reservationMapped = _mapper.Map<ReservationModel, Reservation>(reservation);
                _DAL.UpdateReservation(reservationMapped);
                }
            }
        }

        public void DeleteReservationById(int id)
        {
            if (_DAL.ReservationExistById(id))
                // check if the reservation userId is (the same as the loged user) || admin
                _DAL.DeleteReservationById(id);
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
