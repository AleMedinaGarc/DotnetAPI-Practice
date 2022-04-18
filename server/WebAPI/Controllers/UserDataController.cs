using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using APICarData.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using APICarData.Domain.Interfaces.UserData;

namespace APICarData.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDataController : ControllerBase
    {
        private readonly IUserDataService _service;

        public UserDataController(IUserDataService service)
        {
            _service = service;
        }

        [HttpGet("")]
        [Authorize(Roles = "Administrator, Employee")]
        public ActionResult<UserModel> GetCurrentUserData()
        {
            try
            {
                var user = _service.GetCurrentUserData();
                return Ok(user);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }

        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<UserModel> GetUserDataById(int id)
        {
            try
            {
                var user = _service.GetUserDataById(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }

        }

        [HttpGet("allUsers")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers()
        {
            try
            {
                var allUsers = await _service.GetAllUsers();
                return Ok(allUsers);
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

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteUserById(int id)
        {
            try
            {
                _service.DeleteUserById(id);
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