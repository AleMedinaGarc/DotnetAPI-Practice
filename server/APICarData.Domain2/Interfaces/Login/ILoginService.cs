using APICarData.Domain.Data.Entities;
using APICarData.Domain.Models;

namespace APICarData.Domain.Interfaces.Login
{
    public interface ILoginService
    {
        string Login(GoogleUserDataModel googleUserDataModel);
        void UpdateUser(UserModel userModel);
    }
}
