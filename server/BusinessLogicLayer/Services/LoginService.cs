using APICarData.Domain.Data.Entities;
using System;
using AutoMapper;
using APICarData.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using APICarData.Domain.Interfaces.Login;
using APICarData.Domain.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace APICarData.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginDAL _DAL;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(ILoginDAL DAL, IMapper mapper, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _DAL = DAL;
            _config = config;
            _httpContextAccessor = httpContextAccessor;

        }

        public string Login(GoogleUserDataModel googleUserDataModel)
        {
            GoogleUserData googleUserData = _mapper.Map<GoogleUserData>(googleUserDataModel);
            if (_DAL.CheckUserExist(googleUserData))
            {
                User existingUser = _DAL.GetUserDataById(googleUserData.userId);
                existingUser.lastLogin = DateTime.Now;
                _DAL.UpdateUser(existingUser);
                return GenerateUserToken(existingUser);

            }
            User newUser = RegisterUser(googleUserData);
            return GenerateUserToken(newUser);
        }

        private string GenerateUserToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, user.role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            Console.Write("Access granted to {0} with {1} permissions\n", user.givenName, user.role);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User RegisterUser(GoogleUserData googleUserData)
        {
            User newUser = new User
            {
                lastLogin = DateTime.Now,
                creationDate = DateTime.Now,
                role = "Employee"
            };
            PropertyCopier<GoogleUserData, User>.Copy(googleUserData, newUser);
            _DAL.RegisterUser(newUser);
            return newUser;
        }

        public void UpdateUser(UserModel userModel)
        {
            var currentUser = GetCurrentUser();
            if (currentUser.email == userModel.email)
            {
                User user = _mapper.Map<User>(userModel);
                _DAL.UpdateUser(user);
            }
        }

        private User GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    email = userClaims.FirstOrDefault(o =>
                        o.Type == ClaimTypes.Email)?.Value,
                    role = userClaims.FirstOrDefault(o =>
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
                        if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
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
