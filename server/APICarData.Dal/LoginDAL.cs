using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces.Login;
using APICarData.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APICarData.Dal
{
    public class LoginDAL : ILoginDAL
    {
        private readonly IApiContext _context;
        public LoginDAL(IApiContext context)
        {
            _context = context;
        }
        public void RegisterUser(User user)
        {
            try
            {
                _context.Insert(user);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
        public User GetUserDataById(int id)
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

        public bool CheckUserExist(GoogleUserData googleUserData)
        {
            try
            {
                bool exist = _context.Users.Any(p =>
                    p.UserId == googleUserData.UserId);
                //&& o.Password == userLogin.Password);
                return exist;
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
