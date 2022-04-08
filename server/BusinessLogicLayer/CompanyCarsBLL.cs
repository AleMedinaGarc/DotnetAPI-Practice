using APICarData.DataAccessLayer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using APICarData.BusinessLogicLayer.Models;

namespace APICarData.BusinessLogicLayer
{
    public class CompanyCarsBLL
    {
        private readonly DataAccessLayer.CompanyCarsDAL _DAL;
        private readonly IMapper _mapper;

        public CompanyCarsBLL(DataAccessLayer.CompanyCarsDAL DAL, IMapper mapper)
        {
            _mapper = mapper;
            _DAL = DAL;
        }

        public async Task<IEnumerable<CompanyCarModel>> GetAllCompanyCars()
        {
            try
            {
                var allCars = await _DAL.GetAllCompanyCars();
                return _mapper.Map<IEnumerable<CompanyCar>, IEnumerable<CompanyCarModel>>(allCars);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public void AddCompanyCar(CompanyCarModel car)
        {
            var carToAdd = _mapper.Map<CompanyCarModel, CompanyCar>(car);
            // get data from DTGredis
            // reflect DTGredis with body data
            _DAL.AddCompanyCar(carToAdd);
        }

        public void UpdateCompanyCar(CompanyCarModel car, string id)
        {
            var carToUpdate = _mapper.Map<CompanyCarModel, CompanyCar>(car);
            _DAL.UpdateCompanyCar(carToUpdate, id);
        }
        public void DeleteCompanyCar(string id)
        {
            try
            {
                _DAL.DeleteCompanyCar(id);
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }
    }
}
