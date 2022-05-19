using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces.Login;
using APICarData.Domain.Interfaces;

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
            _context.Insert(user);
        }
    }
}
