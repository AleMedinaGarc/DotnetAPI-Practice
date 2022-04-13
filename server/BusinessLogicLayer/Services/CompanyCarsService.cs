
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

using APICarData.Domain.Models;
using APICarData.Domain.Data.Entities;
using StackExchange.Redis;
using APICarData.Domain.Interfaces.CompanyCars;
using System.Text.Json;
using System.Linq;

namespace APICarData.Services
{
    public class CompanyCarsService : ICompanyCarsService
    {
        private readonly ICompanyCarsDAL _DAL;
        private readonly IMapper _mapper;
        private readonly IConnectionMultiplexer _redis;

        public CompanyCarsService(ICompanyCarsDAL DAL, IMapper mapper, IConnectionMultiplexer redis)
        {
            _mapper = mapper;
            _DAL = DAL;
            _redis = redis;
        }

        public async Task<IEnumerable<CompanyCarModel>> GetAllCompanyCars()
        {
            try
            {
                var allCars = await _DAL.GetAllCompanyCars();
                return _mapper.Map< IEnumerable<CompanyCarModel>>(allCars); ;
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }
        }

        public async Task<IEnumerable<DGTCarModel>> GetAllCompanyCarsExtended(IEnumerable<CompanyCarModel> allCompanyCars)
        {
            List<CompanyCarModel> companyCarList = allCompanyCars.ToList();
            List<DGTCarModel> dgtCarList = new List<DGTCarModel>();
            foreach (CompanyCarModel item in companyCarList)
            {
                // var extendedCarData = await _DAL.GetRedisCarById(item.VIN);
                var extendedCarData = await _redis.GetDatabase().StringGetAsync(item.VIN);
                var dgtCar = JsonSerializer.Deserialize<DGTCar>(extendedCarData);
                var dgtCarModeled = _mapper.Map<DGTCarModel>(dgtCar);
                dgtCarList.Add(dgtCarModeled);
            }
            return dgtCarList.AsEnumerable();
        }

        public void AddCompanyCar(CompanyCarModel car)
        {
            // ITS NOT NEEDED THE EXTENDED CAR INFO
            // TO STORE THE COMPANY CAR DATA
            try
            {
                CompanyCar carToAdd = _mapper.Map<CompanyCar>(car);
                _DAL.AddCompanyCar(carToAdd);
            }
            catch (Exception e)
            { 
                if (e.Source != null)
                    Console.WriteLine("Exception source:", e.Source);
                throw;
            }

        }

        public void UpdateCompanyCar(CompanyCarModel car, string id)
        {
            var carToUpdate = _mapper.Map<CompanyCar>(car);
            if (car.VIN == id)
                _DAL.UpdateCompanyCar(carToUpdate);
        }

        public void DeleteCompanyCarById(string id)
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
