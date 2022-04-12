using APICarData.Domain.Data.Entities;
using APICarData.Domain.Models;

namespace APICarData.Domain.Interfaces.Login
{
    public interface ILoginService
    {
        public string Login(GoogleUserDataModel googleUserData);        
    }
}
