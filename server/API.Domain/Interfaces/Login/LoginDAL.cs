using System;
using System.Collections.Generic;
using System.Text;
using APICarData.Domain.Models;
using 

namespace APICarData.Domain.Interfaces.Login
{
    public interface ILoginDAL
    {
        void RegisterUser(User user);
        bool CheckUserExist(GoogleUserData googleUserData);

    }
}
