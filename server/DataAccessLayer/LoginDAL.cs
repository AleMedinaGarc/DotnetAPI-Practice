using APICarData.DataAccessLayer.Data.ApiContext;
using APICarData.DataAccessLayer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APICarData.DataAccessLayer
{
    public class LoginDAL
    {
        private readonly ApiContext _context;
        public LoginDAL(ApiContext context)
        {
            _context = context;
        }
        public void RegisterUser(User user)
        { 
            _context.Add(user);
            _context.SaveChanges();
        }

        public bool CheckUserExist(GoogleUserData googleUserData)
        {
            return _context.Users.Any(o =>
                o.userId == googleUserData.userId);
            //&& o.Password == userLogin.Password);
        }

    }
}
