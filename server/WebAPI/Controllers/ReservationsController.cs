using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using APICarData.Domain.Models;
using APICarData.Domain.Interfaces.Reservations;
using System.Threading.Tasks;
using System;
using System.Reflection;
using Serilog;

namespace APICarData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _service;

        public ReservationsController(IReservationsService service)
        {
            _service = service;
        }

        [HttpGet("myReservations")]
        [Authorize(Roles = "Administrator,Employee")]
        public async Task<ActionResult<IEnumerable<ReservationModel>>> GetCurrentUserReservations()
        {
            try
            {
                Log.Debug("Searching for reservations...");
                var reservations = await _service.GetCurrentUserReservations();
                Log.Debug("Done.");
                return reservations!=null ? Ok(reservations) : Ok(Array.Empty<string>());
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while fetching user in the database.");
            }
        }

        [HttpGet("userReservations/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<ReservationModel>>> GetUserReservations(string id)
        {
            try
            {
                Log.Debug("Searching for reservations...");
                var reservations = await _service.GetUserReservations(id);
                Log.Debug("Done.");
                return reservations != null ? Ok(reservations) : Ok(Array.Empty<string>());
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while fetching user in the database.");
            }
        }

        [HttpGet("allReservations")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<ReservationModel>>> GetAllReservations()
        {
            try
            {
                Log.Debug("Searching for reservations...");
                var reservations = await _service.GetAllReservations();
                Log.Debug("Done.");
                return reservations != null ? Ok(reservations) : Ok(Array.Empty<string>());

            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while fetching user in the database.");
            }
        }

        [HttpPost("addReservation")]
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult AddReservation([FromBody] ReservationModel reservation)
        {
            try
            {
                Log.Debug("Adding reservation...");
                _service.AddReservation(reservation);
                Log.Debug("Done.");
                return Ok("Entry added.");
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while fetching user in the database.");
            }
        }

        [HttpPut("updateReservation")]
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult UpdateReservation([FromBody] ReservationModel reservation)
        {
            try
            {
                Log.Debug("Updating reservation...");
                _service.UpdateReservation(reservation);
                Log.Debug("Done.");
                return Ok("Entry Updated.");
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while fetching user in the database.");
            }
        }

        [HttpDelete("deleteReservation/{id}")]
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult DeleteReservation(int id)
        {
            try
            {
                Log.Debug("Removing reservation...");
                _service.DeleteReservation(id);
                Log.Debug("Done.");
                return Ok("Entry removed.");
                
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while fetching user in the database.");
            }
        }
    }
}