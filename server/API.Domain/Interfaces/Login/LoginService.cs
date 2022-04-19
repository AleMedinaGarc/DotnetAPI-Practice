using APICarData.Dal.Data.Entities;
using APICarData.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APICarData.Domain.Interfaces.Login
{
    internal interface ILoginService
    {
        string Login(GoogleUserDataModel googleUserData);
        string GenerateUserToken(User user);
        bool Authenticate(GoogleUserData googleUserData);
        void RegisterUser(GoogleUserData googleUserData);
        
    }
}
