using APICarData.Domain.Data.Entities;
using APICarData.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICarData.Domain.Interfaces.UserData
{
    public interface IUserDataService
    {
        UserModel GetCurrentUserData();
        UserModel GetUserDataById(string userId);
        Task<IEnumerable<UserModel>> GetAllUsers();
        bool UpdateUser(UserModel userModel);
        bool DeleteUserById(string userId);
    }
}
