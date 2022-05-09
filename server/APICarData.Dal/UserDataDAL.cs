using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces.UserData;
using APICarData.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APICarData.Dal
{
    public class UserDataDAL : IUserDataDAL
    {
        private readonly IApiContext _context;
        public UserDataDAL(IApiContext context)
        {
            _context = context;
        }
        public User GetUserDataById(string id)
        {
            return _context.Users.FirstOrDefault(p =>
                p.UserId == id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            IEnumerable<User> users = await _context.Users
                .OrderBy(x => x.FullName)
                .ToListAsync();
            return users;
        }

        public void UpdateUser(User user)
        {
            _context.UpdateEntry(user);
        }

        public void DeleteUserById(string userId)
        {
            var user = _context.Users.FirstOrDefault(p => p.UserId == userId);
            _context.Delete(user);
        }

        public bool UserExist(string userId)
        {
            var data = _context.Users.Any(p => p.UserId == userId);
            return data;
        }

        public bool UsersEmpty()
        {
            return !_context.Users.Any();
        }
    }
}
