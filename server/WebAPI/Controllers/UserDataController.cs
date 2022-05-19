using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using APICarData.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using APICarData.Domain.Interfaces.UserData;
using System.Reflection;
using Serilog;

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
                Log.Debug("Getting current user data..");
                var user = _service.GetCurrentUserData();
                Log.Debug("Done.");
                return Ok(user);
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while fetching user in the database.");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        public ActionResult<UserModel> GetUserDataById(string id)
        {
            try
            {
                Log.Debug("Getting user data..");
                var user = _service.GetUserDataById(id);
                Log.Debug("Done.");
                return Ok(user);
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while fetching user in the database.");
            }
        }

        [HttpGet("allUsers")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetAllUsers()
        {
            try
            {
                Log.Debug("Getting all users data..");
                var allUsers = await _service.GetAllUsers();
                Log.Debug("Done.");
                return Ok(allUsers);
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while fetching users in the database.");
            }
        }

        [HttpPut("updateUser")]
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult UpdateUser([FromBody] UserModel userModel)
        {
            try
            {
                Log.Debug("Updating user data..");
                _service.UpdateUser(userModel);
                Log.Debug("Done.");
                return Ok("User updated.");
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while updating user.");
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteUserById(string id)
        {
            try
            {
                Log.Debug("Removing user data..");
                _service.DeleteUserById(id);
                Log.Debug("Done.");
                return Ok("User removed from database");
            }
            catch (AmbiguousMatchException)
            {
                throw new Exception("Exception while removing user.");
            }
        }
    }
}