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
        User GetUserDataById(string id);
        Task<IEnumerable<User>> GetAllUsers();
        void UpdateUser(User user);
        void DeleteUserById(string userId);
        bool UserExist(string userId);
        bool UsersEmpty();
    }
}
