using System;
using System.Linq;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

using APICarData.Data.ApiContext;
using APICarData.Data.Entities;

namespace APICarData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ApiContext _context;

        public LoginController(IConfiguration config, ApiContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] GoogleUserData googleUserData)
        {
            var user = Authenticate(googleUserData);
            if (Authenticate(googleUserData) != null)
                return Ok(Generate(user));
            RegisterUser(googleUserData);
            return Ok(Generate(user));
            //return NotFound("User not found");
        }

        private string Generate(User user)
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

        private User Authenticate(GoogleUserData googleUserData)
        {
            var currentUser = _context.Users.FirstOrDefault(o =>
                o.userId == googleUserData.userId);
            //&& o.Password == userLogin.Password);
            return currentUser != null ? currentUser : null;
        }

        private User RegisterUser(GoogleUserData googleUserData)
        {
            User _user = new User();
            // Using reflection
            PropertyCopier<GoogleUserData, User>.Copy(googleUserData, _user);

            _context.Users.Add(_user);
            _context.SaveChanges();
            return _user;
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