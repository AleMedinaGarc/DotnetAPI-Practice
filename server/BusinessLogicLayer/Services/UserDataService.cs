using APICarData.Domain.Data.Entities;
using System;
using AutoMapper;
using APICarData.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using APICarData.Domain.Interfaces.UserData;
using APICarData.Domain.Interfaces;
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
            var currentEmail = GetCurrentUser().Email;
            var user = _DAL.GetUserDataByEmail(currentEmail);
            return _mapper.Map<UserModel>(user);
        }

        public UserModel GetUserDataById(int userId)
        {
            var user = _DAL.GetUserDataById(userId);
            return _mapper.Map<UserModel>(user);
        }
        
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            var allUsers = await _DAL.GetAllUsers();
            return _mapper.Map<IEnumerable<UserModel>>(allUsers);
        }

        public void UpdateUser(UserModel userModel)
        {
            var currentUser = GetCurrentUser();
            if (currentUser.Email == userModel.Email ||
                currentUser.Role == "Administrator")
            {
                User user = _mapper.Map<User>(userModel);                
                var dbUser = _DAL.GetUserDataById(userModel.UserId);
                PropertyCopier<User, User>.Copy(user, dbUser);
                
                _DAL.UpdateUser(dbUser);

                // maybe relogin needed because of the email identity
            }
        }

        public void DeleteUserById(int userId)
        {
            _DAL.DeleteUserById(userId);
        }

        private User GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    Email = userClaims.FirstOrDefault(o =>
                        o.Type == ClaimTypes.Email)?.Value,
                    Role = userClaims.FirstOrDefault(o =>
                        o.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }

        public class PropertyCopier<TParent, TChild> where TParent : class
                                                         where TChild : class
        {
            public static void Copy(TParent parent, TChild child)
            {
                var parentProperties = parent.GetType().GetProperties();
                var childProperties = child.GetType().GetProperties();

                foreach (var parentProperty in parentProperties)
                {
                    foreach (var childProperty in childProperties)
                    {
                        if (parentProperty.Name == childProperty.Name && 
                            parentProperty.PropertyType == childProperty.PropertyType && 
                            parentProperty.GetValue(parent) != null &&
                            parentProperty.Name != "lastLogin" &&
                            parentProperty.Name != "creationDate")
                        {
                            childProperty.SetValue(child, parentProperty.GetValue(parent));
                            break;
                        }
                    }
                }
            }
        }

    }
}
