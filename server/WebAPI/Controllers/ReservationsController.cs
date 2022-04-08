using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using APICarData.DataAccessLayer.Data.Entities;
using Microsoft.AspNetCore.Http;
using APICarData.BusinessLogicLayer.Models;

namespace APICarData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly BusinessLogicLayer.ReservationsBLL _BLL;

        public ReservationsController(BusinessLogicLayer.ReservationsBLL BLL)
        {
            _BLL = BLL;
        }

        /// <summary>
        /// Return user reservation 
        /// </summary>
        [HttpGet("myReservations")]
        [Authorize(Roles = "Administrator,Employee")]
        public async Task<ActionResult<IEnumerable<ReservationModel>>> GetReservationsByUser()
        {
            var reservations = await _BLL.GetReservationsByUser();
            return Ok(reservations);
            //try
            //{
            //    if (_context.Reservations.Any(p =>
            //        p.userId == GetCurrentUser().userId))
            //    {
            //        return await _context.Reservations.Where(p =>
            //        p.userId == GetCurrentUser().userId).ToListAsync();
            //    }
            //    return NotFound("There's no cars registered in the database.");
            //}
            //catch (Exception e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("Exception source:", e.Source);
            //    throw;
        }

        /// <summary>
        /// Return all reservations in the database
        /// </summary>
        [HttpGet("allReservations")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<ReservationModel>>> GetAllReservations()
        {
            var reservations = await _BLL.GetAllReservations();
            return Ok(reservations);
            //try
            //{
            //    if (_context.Reservations.Any())
            //        return await _context.Reservations.ToListAsync();
            //    return NotFound("There's no reservations registered in the database.");
            //}
            //catch (Exception e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("Exception source:", e.Source);
            //    throw;
            //}
        }
        /// <summary>
        /// Add a car to the database under users username 
        /// </summary>
        [HttpPost("addReservation")]
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult AddReservation([FromBody] ReservationModel reservation)
        {
            _BLL.AddReservation(reservation);
            return Ok();
            //var currentUser = GetCurrentUser();
            //try
            //{
            //    if (reservation.userId == currentUser.userId ||
            //        currentUser.role == "Administrator")
            //    {
            //        _context.Reservations.Add(reservation);
            //        _context.SaveChanges();
            //        return Ok();
            //    }
            //    return Unauthorized("Problem with the credentials, try to log in with a valid account.");
            //}
            //catch (Exception e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("Exception source:", e.Source);
            //    throw;
            //}
        }
        /// <summary>
        /// Update a reservation 
        /// </summary>
        [HttpPut("updateCar/{id}")]
        [Authorize(Roles = "Administrator,GeneralUser")]
        public IActionResult UpdateReservationById([FromBody] ReservationModel reservation, int id)
        {
            _BLL.UpdateReservationById(reservation, id);
            return Ok();
            //var currentUser = GetCurrentUser();
            //try
            //{
            //    if (reservation.userId == currentUser.userId ||
            //        currentUser.role == "Administrator")
            //    {
            //        if (reservation.vin == id)
            //        {
            //            _context.Entry(reservation).State = EntityState.Modified;
            //            _context.SaveChanges();
            //            return Ok();
            //        }
            //        return BadRequest("The route and the body id doesn't match");
            //    }
            //    return Unauthorized("Problem with the credentials, try to log in with a valid account.");
            //}
            //catch (Exception e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("Exception source:", e.Source);
            //    throw;
            }
        
        /// <summary>
        /// Delete a car on the database under users username 
        /// </summary>
        [HttpDelete("deleteReservation/{id}")]
        [Authorize(Roles = "Administrator,GeneralUser")]
        public IActionResult DeleteReservationById(int id)
        {
            _BLL.DeleteReservationById(id);
            return Ok();
        }
            //var currentUser = GetCurrentUser();
            //try
            //{
            //    var reservation = _context.Reservations.FirstOrDefault(p => p.vin == id);
            //    if (reservation != null)
            //    {
            //        if (reservation.userId == currentUser.userId ||
            //            currentUser.role == "Administrator")
            //        {
            //            _context.Reservations.Remove(reservation);
            //            _context.SaveChanges();
            //            return Ok();
            //        }
            //        return Unauthorized("Unauthorized, try to log in with a valid account.");
            //    }
            //    return NotFound("The car doesn't exist in the database.");
            //}
            //catch (Exception e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("Exception source:", e.Source);
            //    throw;
            //}

    }
}