using APICarData.Domain.Data.Entities;
using APICarData.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APICarData.Domain.Interfaces.UserData
{
    public interface IUserDataDAL
    {
        User GetUserDataById(int id);
        User GetUserDataByEmail(string email);
        Task<IEnumerable<User>> GetAllUsers();
        void UpdateUser(User user);
        void DeleteUserById(int userId);
    }
}
