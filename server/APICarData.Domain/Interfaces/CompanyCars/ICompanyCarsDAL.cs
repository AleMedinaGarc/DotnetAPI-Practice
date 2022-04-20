using APICarData.Domain.Data.Entities;
using APICarData.Domain.Models;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICarData.Domain.Interfaces.CompanyCars
{
    public interface ICompanyCarsDAL
    {
        Task<IEnumerable<CompanyCar>> GetAllCompanyCars();
        Task<RedisValue> GetDGTCar(string VIN);
        void AddCompanyCar(CompanyCar car);
        void UpdateCompanyCar(CompanyCar car);
        void DeleteCompanyCar(string id);
        bool CompanyCarsIsEmpty();
        bool CompanyCarExist(string id);
    }
}
