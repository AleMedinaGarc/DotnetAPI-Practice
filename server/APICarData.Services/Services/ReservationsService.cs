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
using APICarData.Domain.Interfaces.CompanyCars;
using APICarData.Domain.Interfaces.UserData;

namespace APICarData.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IReservationsDAL _DAL;
        private readonly IUserDataDAL _users;
        private readonly ICompanyCarsDAL _cars;

        public ReservationsService(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IReservationsDAL DAL,
            IUserDataDAL users,
            ICompanyCarsDAL cars
            )
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _DAL = DAL;
            _users = users;
            _cars = cars;
        }

        public async Task<IEnumerable<ReservationModel>> GetCurrentUserReservations()
        {
            var currentUser = GetCurrentUser();
            if (_DAL.UserHasReservations(currentUser.UserId))
            {
                var reservations = await _DAL.GetUserReservations(currentUser.UserId);
                var reservationsMapped = _mapper.Map<IEnumerable<ReservationModel>>(reservations);
                return reservationsMapped;
            }
            return null;
        }

        public async Task<IEnumerable<ReservationModel>> GetUserReservations(string userId)
        {
            if (_DAL.UserHasReservations(userId))
            {
                var reservations = await _DAL.GetUserReservations(userId);
                var reservationsMapped = _mapper.Map<IEnumerable<ReservationModel>>(reservations);
                return reservationsMapped;
            }
            return null;
        }

        public async Task<IEnumerable<ReservationModel>> GetAllReservations()
        {
            if (!_DAL.ReservationsEmpty())
            {
                var reservations = await _DAL.GetAllReservations();
                var reservationsMapped = _mapper.Map<IEnumerable<ReservationModel>>(reservations);
                return reservationsMapped;
            }
            else
            {
                return null;
            }
        }

        public bool AddReservation(ReservationModel reservation)
        {
            var currentUser = GetCurrentUser();
            if (_cars.CompanyCarExist(reservation.VIN) &&
                _users.UserExist(reservation.UserId))
            {
                if (currentUser.UserId == reservation.UserId ||
                    currentUser.Role == "Administrator")
                {
                    if (!_DAL.CarAlreadyTaken(reservation.VIN))
                    {
                        var reservationMapped = _mapper.Map<Reservation>(reservation);
                        _DAL.AddReservation(reservationMapped);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool UpdateReservation(ReservationModel reservation)
        {
            var currentUser = GetCurrentUser();
            if (_DAL.ReservationExist(reservation.ReservationId))
            {
                if (currentUser.UserId == reservation.UserId ||
                           currentUser.Role == "Administrator")
                {
                    var oldReservation = _DAL.GetReservationById(reservation.ReservationId);
                    if (oldReservation.UserId == reservation.UserId &&
                        oldReservation.VIN == reservation.VIN)
                    {
                        var reservationMapped = _mapper.Map<Reservation>(reservation);
                        _DAL.UpdateReservation(reservationMapped, oldReservation);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool DeleteReservation(int id)
        {
            var currentUser = GetCurrentUser();
            if (_DAL.ReservationExistById(id))
            {
                var reservation = _DAL.GetReservationById(id);
                if (currentUser.UserId == reservation.UserId ||
                               currentUser.Role == "Administrator")
                {
                    _DAL.DeleteReservationById(id);
                    return true;
                }
            }
            return false;
        }
        private User GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var userClaims = identity.Claims;
                return new User
                {
                    UserId = userClaims.FirstOrDefault(o =>
                        o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = userClaims.FirstOrDefault(o =>
                        o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
