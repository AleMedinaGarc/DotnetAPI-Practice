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
using Serilog;

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
            Log.Debug($"Checking if user: {currentUser.UserId} has any reservation..");
            if (_DAL.UserHasReservations(currentUser.UserId))
            {
                var reservations = await _DAL.GetUserReservations(currentUser.UserId);
                Log.Debug("Data found!");
                var reservationsMapped = _mapper.Map<IEnumerable<ReservationModel>>(reservations);
                return reservationsMapped;
            }
            Log.Debug($"There is no reservations in the database, returning empty.");
            return null;
        }

        public async Task<IEnumerable<ReservationModel>> GetUserReservations(string userId)
        {
            Log.Debug($"Checking if user: {userId} has any reservation..");
            if (_DAL.UserHasReservations(userId))
            {
                var reservations = await _DAL.GetUserReservations(userId);
                Log.Debug("Data found!");
                var reservationsMapped = _mapper.Map<IEnumerable<ReservationModel>>(reservations);
                return reservationsMapped;
            }
            Log.Debug("There is no reservations in the database, returning empty.");
            return null;
        }

        public async Task<IEnumerable<ReservationModel>> GetAllReservations()
        {
            Log.Debug("Checking for any reservation in the database..");
            if (!_DAL.ReservationsEmpty())
            {
                var reservations = await _DAL.GetAllReservations();
                Log.Debug("Data found!");
                var reservationsMapped = _mapper.Map<IEnumerable<ReservationModel>>(reservations);
                return reservationsMapped;
            }
            Log.Debug("There is no reservations in the database, returning empty.");
            return null;
        }

        public void AddReservation(ReservationModel reservation)
        {
            var currentUser = GetCurrentUser();
            Log.Debug($"Checking if car {reservation.VIN} exist..");
            if (_cars.CompanyCarExist(reservation.VIN))
            {
                Log.Debug($"Checking if user {reservation.UserId} exist..");
                if (_users.UserExist(reservation.UserId))
                {
                    Log.Debug($"Checking if {reservation.VIN} is already taken..");
                    if (currentUser.UserId == reservation.UserId ||
                        currentUser.Role == "Administrator")
                    {
                        Log.Debug($"Checking if {reservation.VIN} is already taken..");
                        if (!_DAL.CarAlreadyTaken(reservation.VIN))
                        {
                            var reservationMapped = _mapper.Map<Reservation>(reservation);
                            _DAL.AddReservation(reservationMapped);
                        }
                        else
                            throw new InvalidOperationException($"Car {reservation.VIN} already taken.");
                    }
                    else
                        throw new KeyNotFoundException($"User {reservation.UserId} doesn´t exist in the DB.");
                }
                else
                    throw new KeyNotFoundException($"Car {reservation.VIN} doesn´t exist in the DB.");
            }
        }

        public void UpdateReservation(ReservationModel reservation)
        {
            var currentUser = GetCurrentUser();
            Log.Debug($"Checking if reservation {reservation.ReservationId} exist..");
            if (_DAL.ReservationExist(reservation.ReservationId))
            {
                Log.Debug("Checking permissions..");
                if (currentUser.UserId == reservation.UserId ||
                           currentUser.Role == "Administrator")
                {
                    var oldReservation = _DAL.GetReservationById(reservation.ReservationId);
                    Log.Debug("Data found!");
                    Log.Debug("Checking body content..");
                    if (oldReservation.UserId == reservation.UserId &&
                        oldReservation.VIN == reservation.VIN)
                    {
                        var reservationMapped = _mapper.Map<Reservation>(reservation);
                        _DAL.UpdateReservation(reservationMapped, oldReservation);
                    }
                    else
                        throw new InvalidOperationException("Body user id and  car VIN needs to be the same as the old one.");
                }
                else
                    throw new UnauthorizedAccessException($"Unauthorized action from {currentUser.UserId} with {currentUser.Role}.");
            }
            throw new KeyNotFoundException($"Reservation {reservation.ReservationId} not found.");
        }

        public void DeleteReservation(int id)
        {
            var currentUser = GetCurrentUser();
            Log.Debug($"Checking if reservation {id} exist..");
            if (_DAL.ReservationExistById(id))
            {
                var reservation = _DAL.GetReservationById(id);
                Log.Debug("Checking permissions..");
                if (currentUser.UserId == reservation.UserId ||
                               currentUser.Role == "Administrator")
                {
                    _DAL.DeleteReservationById(id);
                }
                else 
                    throw new UnauthorizedAccessException($"Unauthorized action from {currentUser.UserId} with {currentUser.Role}.");
            }
            else
                throw new KeyNotFoundException($"Reservation {id} not found.");
        }
        private User GetCurrentUser()
        {
            Log.Debug("Getting logged user data..");
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
            throw new UnauthorizedAccessException("Logged user not found.");
        }
    }
}
