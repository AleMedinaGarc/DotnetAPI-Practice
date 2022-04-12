using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces.Login;
using APICarData.Domain.Interfaces;
using System;
using System.Linq;

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
        public bool CheckUserExist(GoogleUserData googleUserData)
        {
            try
            {
                bool test = _context.Users.Any(p =>
                    p.userId == googleUserData.userId);
                //&& o.Password == userLogin.Password);
                return test;
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
