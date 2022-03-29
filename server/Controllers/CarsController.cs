using APICarData.Data.CarsContext;
using APICarData.Data.Entities;
using APICarData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APICarData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarsContext context;
        public CarsController(CarsContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Return user cars
        /// </summary>
        [HttpGet("myCars")]
        [Authorize(Roles = "Administrator,GeneralUser")]
        public async Task<ActionResult<IEnumerable<Car>>> GetUserCars()
        {
            try
            {
                if (context.Cars.Any(p =>
                    p.Username == GetCurrentUser().Username))
                {
                    return await context.Cars.Where(p =>
                    p.Username == GetCurrentUser().Username).ToListAsync();
                }
                Console.WriteLine("There's no cars registered in the database.");
                return NotFound();
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
        /// <summary>
        /// Return all cars in the database
        /// </summary>
        [HttpGet("allCars")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
        {
            try
            {
                if (context.Cars.Any())
                    return await context.Cars.ToListAsync();
                Console.WriteLine("There's no cars registered in the database.");
                return NotFound();
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
        [HttpPost("addCar")]
        [Authorize(Roles = "Administrator,GeneralUser")]
        public IActionResult AddCar([FromBody] Car car)
        {
            var currentUser = GetCurrentUser();
            try
            {
                if (car.Username == currentUser.Username ||
                    currentUser.Role == "Administrator")
                {
                    bool plateAlreadyExist = context.Cars.Any(p =>
                       p.PlateNumber == car.PlateNumber);

                    if (!plateAlreadyExist)
                    {
                        context.Cars.Add(car);
                        context.SaveChanges();
                        return Ok();
                    }
                    Console.WriteLine("Plate number already registered.");
                    return Conflict();
                }
                Console.WriteLine("Problem with the credentials, try to log in with a valid account.");
                return Unauthorized();
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
        /// <summary>
        /// Update a car to the database under users username 
        /// </summary>
        [HttpPut("updateCar/{id}")]
        [Authorize(Roles = "Administrator,GeneralUser")]
        public IActionResult UpdateCar([FromBody] Car car, int id)
        {
            var currentUser = GetCurrentUser();
            try
            {
                if (car.Username == currentUser.Username ||
                    currentUser.Role == "Administrator")
                {
                    if (car.Id == id)
                    {
                        context.Entry(car).State = EntityState.Modified;
                        context.SaveChanges();
                        return Ok();
                    }
                    Console.WriteLine("The route and the body id doesn't match: ", car.Id, id);
                    return BadRequest();
                }
                Console.WriteLine("Problem with the credentials, try to log in with a valid account.");
                return Unauthorized();
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
                var car = context.Cars.FirstOrDefault(p => p.Id == id);
                if (car != null)
                {
                    if (car.Username == currentUser.Username ||
                        currentUser.Role == "Administrator")
                    {
                        context.Cars.Remove(car);
                        context.SaveChanges();
                        return Ok();
                    }
                     Console.WriteLine("Unauthorized, try to log in with a valid account.");
                    return Unauthorized();
                }
                Console.WriteLine("The car doesn't exist in the database.");
                return NotFound();
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new UserModel
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}