using APICarData.BusinessLogicLayer.Models;
using APICarData.DataAccessLayer.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace APICarData.BusinessLogicLayer
{
    public class ReservationsBLL
    {
        private readonly DataAccessLayer.ReservationsDAL _DAL;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReservationsBLL(DataAccessLayer.ReservationsDAL DAL, IMapper mapper, IHttpContextAccessor httpContextAccessor)
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
                var reservations = await _DAL.GetReservationsByUser(currentUser.userId);
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
                if (reservation.reservationId == id)
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

            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    email = userClaims.FirstOrDefault(o =>
                        o.Type == ClaimTypes.Email)?.Value,
                    role = userClaims.FirstOrDefault(o =>
                        o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
