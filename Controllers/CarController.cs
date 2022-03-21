using APICarData.Data.CarContex;
using APICarData.Data.Entities;
using APICarData.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

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


        [HttpGet("myCars")]
        [Authorize(Roles = "Administrator,GeneralUser")]
        public IEnumerable<Car> GetUserCars()
        {
            var currentUser = GetCurrentUser();
            var cars = context.Cars.Where(p => p.User == currentUser.Username).ToList();
            return cars;
        }

        [HttpGet("allCars")]
        [Authorize(Roles = "Administrator")]
        public IEnumerable<Car> GetAllCars()
        {
            var cars = context.Cars.ToList();
            return cars;
        }

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
    /*
        [HttpPost("deleteCar")]
        [Authorize(Roles = "Administrator,GeneralUser")]
        public IActionResult DeleteCar(string )
        {
            var currentUser = GetCurrentUser();
            try
            {
                if(car.User ==  currentUser.Username || currentUser.Role == "Administrator")
                {
                    context.Cars.Delete(car);
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
*/
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