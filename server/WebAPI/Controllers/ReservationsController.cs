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
                return Ok(reservations);
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
                return Ok(reservations);
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
                _service.AddReservation(reservation);
                return Ok();
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        [HttpPut("updateReservation/{id}")]
        [Authorize(Roles = "Administrator,GeneralUser")]
        public IActionResult UpdateReservationById([FromBody] ReservationModel reservation, int id)
        {
            try
            {
                _service.UpdateReservationById(reservation, id);
                return Ok();
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        [HttpDelete("deleteReservation/{id}")]
        [Authorize(Roles = "Administrator,GeneralUser")]
        public IActionResult DeleteReservationById(int id)
        {
            try
            {
                _service.DeleteReservationById(id);
                return Ok();
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