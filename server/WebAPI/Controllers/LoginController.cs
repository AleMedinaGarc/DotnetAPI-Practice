using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using APICarData.Domain.Models;
using APICarData.Domain.Interfaces.Login;
using Serilog;
using System.Reflection;

namespace APICarData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;


        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] GoogleUserDataModel googleUserDataModel)
        {
            try
            {
                Log.Debug("Searching for user...");
                string jwt = _service.Login(googleUserDataModel);
                Log.Debug($"Returning token:{jwt}");
                return Ok(new { token = jwt });
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while fetching user in the database.");
            }
        }
    }
}