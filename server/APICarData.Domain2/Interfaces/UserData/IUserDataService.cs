using APICarData.Domain.Data.Entities;
using APICarData.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICarData.Domain.Interfaces.UserData
{
    public interface IUserDataService
    {
        UserModel GetCurrentUserData();
        UserModel GetUserDataById(int userId);
        Task<IEnumerable<UserModel>> GetAllUsers();
        void UpdateUser(UserModel userModel);
        void DeleteUserById(int userId);
    }
}
