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
            try
            {
                return _context.Users.FirstOrDefault(p =>
                    p.UserId == id);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        // This should be fixed at finail product
        public User GetUserDataByEmail(string email)
        {
            try
            {
                return _context.Users.FirstOrDefault(p =>
                    p.Email == email);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                IEnumerable<User> users = await _context.Users
                    .OrderBy(x => x.FullName)
                    .ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                _context.UpdateEntry(user);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public void DeleteUserById (string userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(p => p.UserId == userId);
                _context.Delete(user);
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }

        public bool UserExist(string userId)
        {
            try
            {
                return _context.Users.Any(p => p.UserId == userId);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public bool UsersEmpty()
        {
            try
            {
                return !_context.Users.Any();
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

    }
}
