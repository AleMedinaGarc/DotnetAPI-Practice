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
    public class CompanyCarsController : ControllerBase
    {
        private readonly ApiContext _context;
        public CompanyCarsController(ApiContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Return all company cars in the database
        /// </summary>
        [HttpGet("allCars")]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<ActionResult<IEnumerable<CompanyCar>>> GetAllCars()
        {
            try
            {
                if (_context.CompanyCars.Any())
                    return await _context.CompanyCars.ToListAsync();
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
        /// Add a car DGT car to the database
        /// </summary>
        // [HttpPost("addCar")]
        // [Authorize(Roles = "Administrator")]
        // public IActionResult AddCar([FromBody] Car car)
        // {
        //     var currentUser = GetCurrentUser();
        //     try
        //     {
        //         if (car.Username == currentUser.Username ||
        //             currentUser.Role == "Administrator")
        //         {
        //             bool plateAlreadyExist = _context.Cars.Any(p =>
        //                p.PlateNumber == car.PlateNumber);

        //             if (!plateAlreadyExist)
        //             {
        //                 _context.Cars.Add(car);
        //                 _context.SaveChanges();
        //                 return Ok();
        //             }
        //             return Conflict("Plate number already registered.");
        //         }
        //         return Unauthorized("Problem with the credentials, try to log in with a valid account.");
        //     }
        //     catch (Exception e)
        //     {
        //         if (e.Source != null)
        //             Console.WriteLine("Exception source:", e.Source);
        //         throw;
        //     }
        // }
        /// <summary>
        /// Update a car to the database 
        /// </summary>
        [HttpPut("updateCar/{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateCar([FromBody] CompanyCar car, string id)
        {
            try
            {
                if (car.VIN == id)
                {
                    _context.Entry(car).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Ok();
                }
                return BadRequest("The route and the body id doesn't match");
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
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteCar(int id)
        {
            try
            {
                var car = _context.Reservations.FirstOrDefault(p => p.vin == id);
                if (car != null)
                {

                    _context.Reservations.Remove(car);
                    _context.SaveChanges();
                    return Ok();

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
    }
}