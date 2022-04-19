using APICarData.Domain.Data.Entities;
using APICarData.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICarData.Domain.Interfaces.Login
{
    public interface ILoginService
    {
        string Login(GoogleUserDataModel googleUserDataModel);

    }
}
