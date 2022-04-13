using APICarData.Domain.Data.Entities;
using APICarData.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace APICarData.Domain.Interfaces.Login
{
    public interface ILoginDAL
    {
        void RegisterUser(User user);
        bool CheckUserExist(GoogleUserData googleUserData);
        User GetUserDataById(int id);
        void UpdateUser(User user);
    }
}
