using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using APICarData.Domain.Models;

namespace APICarData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyCarsController : ControllerBase
    {
        private readonly Services.CompanyCarsService _service;

        public CompanyCarsController(Services.CompanyCarsService service)
        {
            _service = service;
        }
   
        [HttpGet("allCars")]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<ActionResult<IEnumerable<CompanyCarModel>>> GetAllCompanyCars()
        {
            var result = await _service.GetAllCompanyCars();
            return Ok(result);

        }
        /// <summary>
        /// Add a car DGT car to the database
        /// </summary>
        [HttpPost("addCar")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddCompanyCar([FromBody] CompanyCarModel car)
        {
            _service.AddCompanyCar(car);
            return Ok();
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
            //    }
        }
        /// <summary>
        /// Update a car to the database 
        /// </summary>
        [HttpPut("updateCar/{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateCompanyCar([FromBody] CompanyCarModel car, string id)
        {
            _service.UpdateCompanyCar(car, id);
            return Ok();
            //try
            //{
            //    if (car.VIN == id)
            //    {
            //        _context.Entry(car).State = EntityState.Modified;
            //        _context.SaveChanges();
            //        return Ok();
            //    }
            //    return BadRequest("The route and the body id doesn't match");
            //}

            //catch (Exception e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("Exception source:", e.Source);
            //    throw;
            //}
        }
        /// <summary>
        /// Delete a car on the database under users username 
        /// </summary>
        [HttpDelete("deleteCar/{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteCompanyCar(string id)
        {
            _service.DeleteCompanyCar(id);
            return Ok();
            //try
            //{
            //    var car = _context.Reservations.FirstOrDefault(p => p.vin == id);
            //    if (car != null)
            //    {

            //        _context.Reservations.Remove(car);
            //        _context.SaveChanges();
            //        return Ok();

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
}