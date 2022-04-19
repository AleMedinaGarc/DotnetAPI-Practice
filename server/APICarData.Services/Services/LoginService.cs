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
using System.Threading.Tasks;
using System.Collections.Generic;

namespace APICarData.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginDAL _DAL;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;    

        public LoginService(ILoginDAL DAL, IMapper mapper, IConfiguration config)
        {
            _mapper = mapper;
            _DAL = DAL;
            _config = config;
        }

        public string Login(GoogleUserDataModel googleUserDataModel)
        {
            GoogleUserData googleUserData = _mapper.Map<GoogleUserData>(googleUserDataModel);
            if (_DAL.CheckUserExist(googleUserData))
            {
                User existingUser = _DAL.GetUserDataById(googleUserData.UserId);
                existingUser.LastLogin = DateTime.Now;
                _DAL.UpdateUser(existingUser);
                return GenerateUserToken(existingUser);

            }
            User newUser = RegisterUser(googleUserData);
            return GenerateUserToken(newUser);
        }        

        private User RegisterUser(GoogleUserData googleUserData)
        {
            User newUser = new User
            {
                LastLogin = DateTime.Now,
                CreationDate = DateTime.Now,
                Role = "Employee"
            };
            PropertyCopier<GoogleUserData, User>.Copy(googleUserData, newUser);
            _DAL.RegisterUser(newUser);
            return newUser;
        }
        
        private string GenerateUserToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            Console.Write("Access granted to {0} with {1} permissions\n", user.GivenName, user.Role);
            return new JwtSecurityTokenHandler().WriteToken(token);
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
