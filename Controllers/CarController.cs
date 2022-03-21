using APICarData.Data.CarContex;
using APICarData.Data.Entities;
using APICarData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace APICarData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarContext context;
        public CarController(CarContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Return user cars
        /// </summary>
        [HttpGet("myCars")]
        [Authorize(Roles = "Administrator,GeneralUser")]
        public IEnumerable<Car> GetUserCars()
        {
            var currentUser = GetCurrentUser();
            var cars = context.Cars.Where(p => p.User == currentUser.Username).ToList();
            return cars;
        }
        /// <summary>
        /// Return all cars in the database
        /// </summary>
        [HttpGet("allCars")]
        [Authorize(Roles = "Administrator")]
        public IEnumerable<Car> GetAllCars()
        {
            var cars = context.Cars.ToList();
            return cars;
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
                if(car.User ==  currentUser.Username || currentUser.Role == "Administrator")
                {
                    context.Cars.Add(car);
                    context.SaveChanges();
                    return Ok();
                }
                return BadRequest();

            } catch (Exception e)
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
                if(car.Id == id  && 
                  (car.User ==  currentUser.Username || 
                   currentUser.Role == "Administrator"))
                {
                    context.Entry(car).State = EntityState.Modified;;
                    context.SaveChanges();
                    return Ok();
                }
                return BadRequest();

            } catch (Exception e)
            { 
                if (e.Source != null)
                Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
        /// <summary>
        /// Delete a car on the database under users username 
        /// </summary>
        [HttpPost("deleteCar/{id}")]
        [Authorize(Roles = "Administrator,GeneralUser")]
        public IActionResult DeleteCar(int id)
        {
            var currentUser = GetCurrentUser();
            try
            {   
                var car = context.Cars.FirstOrDefault(p => p.Id == id);
                if(car.User ==  currentUser.Username || currentUser.Role == "Administrator")
                {
                    context.Cars.Remove(car);
                    context.SaveChanges();
                    return Ok();
                }
                return BadRequest();

            } catch (Exception e)
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