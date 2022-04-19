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
                var allCompanyCarsExtended = await _service.GetAllCompanyCarsExtended(allCompanyCars);
                object[] array = { allCompanyCars, allCompanyCarsExtended };
                return Ok(array);
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
        public IActionResult AddCompanyCar([FromBody] CompanyCarModel car)
        {
            try
            {
                _service.AddCompanyCar(car);
                return Ok();
            }

            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }

        }

        [HttpPut("updateCar/{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult UpdateCompanyCar([FromBody] CompanyCarModel car, string id)
        {
            try
            {
                _service.UpdateCompanyCar(car, id);
                return Ok();
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
                _service.DeleteCompanyCarById(id);
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