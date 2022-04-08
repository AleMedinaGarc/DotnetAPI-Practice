using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using APICarData.BusinessLogicLayer.Models;

namespace APICarData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BusinessLogicLayer.LoginBLL _BLL;

        public LoginController(BusinessLogicLayer.LoginBLL BLL)
        {
            _BLL = BLL;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] GoogleUserDataModel googleUserData)
        {
            try
            {
                string _JWT = _BLL.Login(googleUserData);
                return Ok(_JWT);
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