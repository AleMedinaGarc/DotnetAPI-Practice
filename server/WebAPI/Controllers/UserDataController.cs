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
                if (user != null)
                    return Ok(user);
                return BadRequest("User not found.");
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
        public ActionResult<UserModel> GetUserDataById(string id)
        {
            try
            {
                var user = _service.GetUserDataById(id);
                if (user != null)
                    return Ok(user);
                return BadRequest("User not found.");
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
                if (allUsers != null)
                    return Ok(allUsers);
                return NotFound("User not found.");
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }

        }

        [HttpPut("updateUser")]
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult UpdateUser([FromBody] UserModel userModel)
        {
            try
            {
                bool result = _service.UpdateUser(userModel);
                if (result)
                    return Ok("User updated.");
                return BadRequest("User not found or permission denied.");
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
        public IActionResult DeleteUserById(string id)
        {
            try
            {
                bool result = _service.DeleteUserById(id);
                if (result)
                    return Ok("User removed from database");
                return BadRequest("User not found or permission denied.");
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