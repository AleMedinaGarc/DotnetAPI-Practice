using APICarData.Domain.Data.Entities;
using AutoMapper;
using APICarData.Domain.Models;
using System.Security.Claims;
using APICarData.Domain.Interfaces.UserData;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Serilog;
using System;

namespace APICarData.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly IUserDataDAL _DAL;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserDataService(IUserDataDAL DAL, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _DAL = DAL;
            _httpContextAccessor = httpContextAccessor;

        }
       
        public UserModel GetCurrentUserData()
        {
            var currentUser = GetCurrentUser();
            Log.Debug("Checking user exist..");
            if (_DAL.UserExist(currentUser.UserId))
            {
                var user = _DAL.GetUserDataById(currentUser.UserId);
                Log.Debug("Data found!");
                return _mapper.Map<UserModel>(user);
            }
            throw new KeyNotFoundException("User not found.");
        }

        public UserModel GetUserDataById(string userId)
        {
            Log.Debug("Checking user exist..");
            if (_DAL.UserExist(userId))
            {
                var user = _DAL.GetUserDataById(userId);
                Log.Debug("Data found!");
                return _mapper.Map<UserModel>(user);
            }
            throw new KeyNotFoundException("User not found.");
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            Log.Debug("Checking any user exist..");
            if (!_DAL.UsersEmpty())
            {
                var allUsers = await _DAL.GetAllUsers();
                Log.Debug("Data found!");
                return _mapper.Map<IEnumerable<UserModel>>(allUsers);
            }
            throw new KeyNotFoundException("User table is empty.");
        }

        public void UpdateUser(UserModel userModel)
        {
            var currentUser = GetCurrentUser();
            Log.Debug("Checking user exist..");
            if (_DAL.UserExist(userModel.UserId))
            {
                Log.Debug($"Checking permissions of user {currentUser.UserId}");
                if (currentUser.UserId == userModel.UserId ||
                currentUser.Role == "Administrator")
                {
                    User user = _mapper.Map<User>(userModel);
                    Log.Debug("Getting old data..");
                    var dbUser = _DAL.GetUserDataById(userModel.UserId);
                    Log.Debug("Data found!");
                    PropertyCopier<User, User>.Copy(user, dbUser);
                    Log.Debug("Saving new data data..");
                    _DAL.UpdateUser(dbUser);
                }
                else
                    throw new UnauthorizedAccessException($"Unauthorized action from {currentUser.UserId} with {currentUser.Role}.");
            }
            else
                throw new KeyNotFoundException($"User {userModel.UserId} not found.");
        }

        public void DeleteUserById(string userId)
        {
            var currentUser = GetCurrentUser();
            Log.Debug("Checking user exist..");
            if (_DAL.UserExist(currentUser.UserId))
            {
                Log.Debug($"Checking permissions of user {currentUser.UserId}");
                if (currentUser.UserId == userId ||
                currentUser.Role == "Administrator")
                {
                    Log.Debug("Removing user..");
                    _DAL.DeleteUserById(userId);
                }
                throw new UnauthorizedAccessException($"Unauthorized action from {currentUser.UserId} with {currentUser.Role}.");
            }
            throw new KeyNotFoundException("User not found.");
        }

        private User GetCurrentUser()
        {
            Log.Debug("Getting logged user data..");
            if (_httpContextAccessor.HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    UserId = userClaims.FirstOrDefault(o =>
                        o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = userClaims.FirstOrDefault(o =>
                        o.Type == ClaimTypes.Role)?.Value
                };
            }
            throw new UnauthorizedAccessException("Logged user not found");
        }
    }
}
