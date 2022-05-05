using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using APICarData.Domain.Models;
using System;
using APICarData.Domain.Interfaces.CompanyCars;

namespace APICarData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyCarsController : ControllerBase
    {
        private readonly ICompanyCarsService _service;

        public CompanyCarsController(ICompanyCarsService service)
        {
            _service = service;
        }

        [HttpGet("allCars")]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<ActionResult<IEnumerable<CompanyCarModel>>> GetAllCompanyCars()
        {
            try
            {
                var allCompanyCars = await _service.GetAllCompanyCars();
                if (allCompanyCars == null)
                    return BadRequest("There is no cars in the database.");

                var allCompanyCarsExtended = await _service.GetAllCompanyCarsExtended(allCompanyCars);
                if (allCompanyCarsExtended == null)
                    return BadRequest("The cars registered has no extended DGT information.");

                object[] merge = { allCompanyCars, allCompanyCarsExtended };
                return Ok(merge);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }

        }
        [HttpGet("getCar/{id}")]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<ActionResult<IEnumerable<CompanyCarModel>>> GetCarById()
        {
            try
            {
                var allCompanyCars = await _service.GetAllCompanyCars();
                if (allCompanyCars == null)
                    return BadRequest("There is no cars in the database.");

                var allCompanyCarsExtended = await _service.GetAllCompanyCarsExtended(allCompanyCars);
                if (allCompanyCarsExtended == null)
                    return BadRequest("The cars registered has no extended DGT information.");

                object[] merge = { allCompanyCars, allCompanyCarsExtended };
                return Ok(merge);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }

        }

        [HttpPost("addCar")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddCompanyCar([FromBody] CompanyCarModel car)
        {
            try
            {
                bool result = await _service.AddCompanyCar(car);
                if (result)
                    return Ok("Entry added.");
                return Conflict("Entry doesnt exist in the DGT or already in the database.");
            }

            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }

        }

        [HttpPut("updateCar")]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateCompanyCar([FromBody] CompanyCarModel car)
        {
            try
            {
                bool result = _service.UpdateCompanyCar(car);
                if (result)
                    return Ok("Car updated.");
                return NotFound("Car not found in the database.");
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }

        }

        [HttpDelete("deleteCar/{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteCompanyCar(string id)
        {
            try
            {
                bool result = _service.DeleteCompanyCarById(id);
                if (result)
                    return Ok("Car updated.");
                return NotFound("Car not found in the database.");

            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }


        //[HttpDelete("getDGTCars")]
        //[Authorize(Roles = "Administrator")]
        //public async Task<ActionResult<IEnumerable<DGTCarModel>>> GetAllDGTCars()
        //{
            //try
            //{
            //    var allCompanyCars = await _service.GetAllDGTCars();
            //    if (allCompanyCars == null)
            //        return BadRequest("There is no cars in the database.");
            //    return Ok(allCompanyCars);
            //}
            //catch (Exception e)
            //{
            //    if (e.Source != null)
            //        Console.WriteLine("Exception source:", e.Source);
            //    throw;
            //}

        //}
    }
}