using APICarData.DataAccessLayer.Data.Entities;
using System;
using AutoMapper;
using APICarData.BusinessLogicLayer.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace APICarData.BusinessLogicLayer
{
    public class LoginBLL
    {
        private readonly DataAccessLayer.LoginDAL _DAL;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public LoginBLL(DataAccessLayer.LoginDAL DAL, IMapper mapper)
        {
            _mapper = mapper;
            _DAL = DAL;
        }

        public string Login(GoogleUserDataModel googleUserData)
        {
            GoogleUserData _mappedGoogleData = _mapper.Map<GoogleUserDataModel, GoogleUserData>(googleUserData);
            User _user = new User();
            PropertyCopier<GoogleUserData, User>.Copy(_mappedGoogleData, _user);

            if (Authenticate(_mappedGoogleData))
                return GenerateUserToken(_user);
            RegisterUser(_mappedGoogleData);
            return GenerateUserToken(_user);
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
            User _user = new User();
            PropertyCopier<GoogleUserData, User>.Copy(googleUserData, _user);

            _DAL.RegisterUser(_user);
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
