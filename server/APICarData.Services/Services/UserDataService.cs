using APICarData.Domain.Data.Entities;
using AutoMapper;
using APICarData.Domain.Models;
using System.Security.Claims;
using APICarData.Domain.Interfaces.UserData;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

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
            if (_DAL.UserExist(currentUser.UserId))
            {
                var user = _DAL.GetUserDataById(currentUser.UserId);
                return _mapper.Map<UserModel>(user);
            }
            return null;
        }

        public UserModel GetUserDataById(string userId)
        {
            if (_DAL.UserExist(userId))
            {
                var user = _DAL.GetUserDataById(userId);
                return _mapper.Map<UserModel>(user);
            }
            return null;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            if (!_DAL.UsersEmpty())
            {
                var allUsers = await _DAL.GetAllUsers();
                return _mapper.Map<IEnumerable<UserModel>>(allUsers);
            }
            return null;
        }

        public bool UpdateUser(UserModel userModel)
        {
            var currentUser = GetCurrentUser();
            if (_DAL.UserExist(currentUser.UserId) &&
                (currentUser.UserId == userModel.UserId ||
                currentUser.Role == "Administrator"))
            {
                User user = _mapper.Map<User>(userModel);
                var dbUser = _DAL.GetUserDataById(userModel.UserId);
                PropertyCopier<User, User>.Copy(user, dbUser);

                _DAL.UpdateUser(dbUser);
                return true;
            }
            return false;
        }

        public bool DeleteUserById(string userId)
        {
            var currentUser = GetCurrentUser();
            if (_DAL.UserExist(currentUser.UserId) &&
                (currentUser.UserId == userId ||
                currentUser.Role == "Administrator"))
            {
                _DAL.DeleteUserById(userId);
                return true;
            }
            return false;
        }

        private User GetCurrentUser()
        {
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
            return null;
        }
    }
}
