using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using APICarData.Domain.Models;
using System;
using APICarData.Domain.Interfaces.CompanyCars;
using Serilog;
using System.Reflection;

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
                Log.Debug("Getting all car data from database..");
                var allCompanyCars = await _service.GetAllCompanyCars();
                Log.Debug("Getting all car data from dgt..");
                var allCompanyCarsExtended = await _service.GetAllCompanyCarsExtended(allCompanyCars);
                Log.Debug("Merging data..");
                object[] merge = { allCompanyCars, allCompanyCarsExtended };
                Log.Debug("Done.");
                return allCompanyCars != null ? Ok(merge) : Ok(Array.Empty<string>());
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while getting all cars data.");
            }

        }
        [HttpGet("getCar/{id}")]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<ActionResult<IEnumerable<CompanyCarModel>>> GetCarById()
        {
            try
            {
                Log.Debug("Getting car data from database..");
                var allCompanyCars = await _service.GetAllCompanyCars();
                Log.Debug("Getting car data from dgt..");
                var allCompanyCarsExtended = await _service.GetAllCompanyCarsExtended(allCompanyCars);
                Log.Debug("Merging data..");
                object[] merge = { allCompanyCars, allCompanyCarsExtended };
                Log.Debug("Done.");
                return allCompanyCars != null ? Ok(merge) : Ok(Array.Empty<string>());
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while getting car info.");
            }

        }

        [HttpPost("addCar")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddCompanyCar([FromBody] CompanyCarModel car)
        {
            try
            {
                Log.Debug("Adding car to database..");
                await _service.AddCompanyCar(car);
                Log.Debug("Done.");
                return Ok("Entry added.");
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while adding car.");
            }

        }

        [HttpPut("updateCar")]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateCompanyCar([FromBody] CompanyCarModel car)
        {
            try
            {
                Log.Debug("Updating car from database..");
                _service.UpdateCompanyCar(car);
                Log.Debug("Done.");
                return Ok("Car updated.");
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while updating car.");
            }
        }

        [HttpDelete("deleteCar/{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteCompanyCar(string id)
        {
            try
            {
                Log.Debug("Removing car from database..");
                _service.DeleteCompanyCarById(id);
                Log.Debug("Done.");
                return Ok("Car removed.");

            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while removing car.");
            }
        }
    }
}