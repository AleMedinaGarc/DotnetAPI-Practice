using APICarData.Domain.Data.Entities;
using APICarData.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICarData.Domain.Interfaces.CompanyCars
{
    public interface ICompanyCarsService
    {
        Task<IEnumerable<CompanyCarModel>> GetAllCompanyCars();
        Task AddCompanyCar(CompanyCarModel car);
        Task<IEnumerable<DGTCarModel>> GetAllCompanyCarsExtended(IEnumerable<CompanyCarModel> allCompanyCars);
        void UpdateCompanyCar(CompanyCarModel car);
        void DeleteCompanyCarById(string id);
        // Task<IEnumerable<DGTCarModel>> GetAllDTGCars();
    }
}
