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
    }
}
