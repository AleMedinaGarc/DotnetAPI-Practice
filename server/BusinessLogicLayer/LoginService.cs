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

        public string Login(GoogleUserDataModel googleUserData)
        {
            GoogleUserData mappedGoogleData = _mapper.Map<GoogleUserData>(googleUserData);
            
            User user = new User();
            PropertyCopier<GoogleUserData, User>.Copy(mappedGoogleData, user);

            user.lastLogin = DateTime.Now;
            user.role = "Employee";

            if (Authenticate(mappedGoogleData))
            {
                // UpdateUser(user);
                return GenerateUserToken(user);
            }
            user.creationDate = DateTime.Now;
            RegisterUser(mappedGoogleData);
            return GenerateUserToken(user);
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

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool Authenticate(GoogleUserData googleUserData)
        {
            return _DAL.CheckUserExist(googleUserData);
        }

        private void RegisterUser(GoogleUserData googleUserData)
        {
            User user = new User();
            PropertyCopier<GoogleUserData, User>.Copy(googleUserData, user);
            _DAL.RegisterUser(user);
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
