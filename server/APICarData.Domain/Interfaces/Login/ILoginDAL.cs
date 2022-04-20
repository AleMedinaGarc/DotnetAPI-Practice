using APICarData.Domain.Data.Entities;
using APICarData.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APICarData.Domain.Interfaces.Login
{
    public interface ILoginDAL
    {
        void RegisterUser(User user);
    }
}
