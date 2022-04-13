using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using APICarData.Domain.Models;
using APICarData.Domain.Interfaces.Login;

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
                string _JWT = _service.Login(googleUserDataModel);
                return Ok(_JWT);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        [HttpPut("updateUser/{id}")]
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult UpdateUser([FromBody] UserModel userModel)
        {
            try
            {
                _service.UpdateUser(userModel);
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