using APICarData.Domain.Data.Entities;
using APICarData.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICarData.Domain.Interfaces.CompanyCars
{
    public interface ICompanyCarsService
    {
        Task<IEnumerable<CompanyCarModel>> GetAllCompanyCars();
        void AddCompanyCar(CompanyCarModel car);
        void UpdateCompanyCar(CompanyCarModel car, string id);
        void DeleteCompanyCarById(string id);
        Task<IEnumerable<DGTCarModel>> GetAllCompanyCarsExtended(IEnumerable<CompanyCarModel> allCompanyCars);
    }
}
