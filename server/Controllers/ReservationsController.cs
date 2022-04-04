using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using APICarData.Data.Entities;
using APICarData.Data.ApiContext;

namespace APICarData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ApiContext _context;
        public ReservationsController(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Return user reservation 
        /// </summary>
        [HttpGet("myReservations")]
        [Authorize(Roles = "Administrator,Employee")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetUserReservations()
        {
            try
            {
                if (_context.Reservations.Any(p =>
                    p.userId == GetCurrentUser().userId))
                {
                    return await _context.Reservations.Where(p =>
                    p.userId == GetCurrentUser().userId).ToListAsync();
                }
                return NotFound("There's no cars registered in the database.");
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
        /// <summary>
        /// Return all reservations in the database
        /// </summary>
        [HttpGet("allReservations")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAllReservations()
        {
            try
            {
                if (_context.Reservations.Any())
                    return await _context.Reservations.ToListAsync();
                return NotFound("There's no reservations registered in the database.");
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
        /// <summary>
        /// Add a car to the database under users username 
        /// </summary>
        [HttpPost("addReservation")]
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult AddReservation([FromBody] Reservation reservation)
        {
            var currentUser = GetCurrentUser();
            try
            {
                if (reservation.userId == currentUser.userId ||
                    currentUser.role == "Administrator")
                {
                    _context.Reservations.Add(reservation);
                    _context.SaveChanges();
                    return Ok();
                }
                return Unauthorized("Problem with the credentials, try to log in with a valid account.");
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
        /// <summary>
        /// Update a reservation 
        /// </summary>
        [HttpPut("updateCar/{id}")]
        [Authorize(Roles = "Administrator,GeneralUser")]
        public IActionResult UpdateCar([FromBody] Reservation reservation, int id)
        {
            var currentUser = GetCurrentUser();
            try
            {
                if (reservation.userId == currentUser.userId ||
                    currentUser.role == "Administrator")
                {
                    if (reservation.vin == id)
                    {
                        _context.Entry(reservation).State = EntityState.Modified;
                        _context.SaveChanges();
                        return Ok();
                    }
                    return BadRequest("The route and the body id doesn't match");
                }
                return Unauthorized("Problem with the credentials, try to log in with a valid account.");
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
        /// <summary>
        /// Delete a car on the database under users username 
        /// </summary>
        [HttpDelete("deleteCar/{id}")]
        [Authorize(Roles = "Administrator,GeneralUser")]
        public IActionResult DeleteCar(int id)
        {
            var currentUser = GetCurrentUser();
            try
            {
                var reservation = _context.Reservations.FirstOrDefault(p => p.vin == id);
                if (reservation != null)
                {
                    if (reservation.userId == currentUser.userId ||
                        currentUser.role == "Administrator")
                    {
                        _context.Reservations.Remove(reservation);
                        _context.SaveChanges();
                        return Ok();
                    }
                    return Unauthorized("Unauthorized, try to log in with a valid account.");
                }
                return NotFound("The car doesn't exist in the database.");
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
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    userId = userClaims.FirstOrDefault(o =>
                        o.Type == ClaimTypes.userId)?.Value,
                    role = userClaims.FirstOrDefault(o =>
                        o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}