using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using APICarData.Domain.Models;
using APICarData.Domain.Interfaces.Reservations;
using System.Threading.Tasks;
using System;

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
                var reservations = await _service.GetCurrentUserReservations();
                if (reservations != null)
                    return Ok(reservations);
                return NotFound("You have no reservations in the database.");
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        [HttpGet("userReservations/{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<ReservationModel>>> GetUserReservations(string id)
        {
            try
            {
                String[] empty = Array.Empty<string>();
                var reservations = await _service.GetUserReservations(id);
                if (reservations != null)
                    return Ok(reservations);
                return Ok(empty);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        [HttpGet("allReservations")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<ReservationModel>>> GetAllReservations()
        {
            try
            {
                var reservations = await _service.GetAllReservations();
                if (reservations != null)
                    return Ok(reservations);
                return NotFound("There is no reservations in the database.");
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        [HttpPost("addReservation")]
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult AddReservation([FromBody] ReservationModel reservation)
        {
            try
            {
                bool result = _service.AddReservation(reservation);
                if (result)
                    return Ok("Entry added.");
                return BadRequest("Permission denied or car already taken.");
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        [HttpPut("updateReservation")]
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult UpdateReservation([FromBody] ReservationModel reservation)
        {
            try
            {
                bool result = _service.UpdateReservation(reservation);
                if (result)
                    return Ok("Entry Updated.");
                return BadRequest("Permission denied or entry missmatch.");
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        [HttpDelete("deleteReservation/{id}")]
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult DeleteReservation(int id)
        {
            try
            {
                bool result = _service.DeleteReservation(id);
                if (result)
                    return Ok("Entry removed.");
                return BadRequest("Permission denied or reservation doesn't exist.");
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
    }
}