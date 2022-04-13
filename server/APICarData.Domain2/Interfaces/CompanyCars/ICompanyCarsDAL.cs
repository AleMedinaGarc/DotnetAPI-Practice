using APICarData.Domain.Data.Entities;
using APICarData.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICarData.Domain.Interfaces.CompanyCars
{
    public interface ICompanyCarsDAL
    {
        Task<IEnumerable<CompanyCar>> GetAllCompanyCars();
        void AddCompanyCar(CompanyCar car);
        void UpdateCompanyCar(CompanyCar car);
        void DeleteCompanyCar(string id);
        bool CompanyCarsAny();
        bool CompanyCarsIsEmpty();
    }
}
