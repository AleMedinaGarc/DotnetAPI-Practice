using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using APICarData.Domain.Models;
using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces.CompanyCars;
using System.Text.Json;
using System.Linq;
using Serilog;

namespace APICarData.Services
{
    public class CompanyCarsService : ICompanyCarsService
    {
        private readonly ICompanyCarsDAL _DAL;
        private readonly IMapper _mapper;

        public CompanyCarsService(ICompanyCarsDAL DAL, IMapper mapper)
        {
            _mapper = mapper;
            _DAL = DAL;
        }

        public async Task<IEnumerable<CompanyCarModel>> GetAllCompanyCars()
        {
            Log.Debug("Checking for any car in the database..");
            if (!_DAL.CompanyCarsIsEmpty())
            {
                var allCars = await _DAL.GetAllCompanyCars();
                Log.Debug("Data found");
                return _mapper.Map<IEnumerable<CompanyCarModel>>(allCars);
            }
            Log.Debug("There is no cars in the database, returning null.");
            return null;
        }

        public async Task<IEnumerable<DGTCarModel>> GetAllCompanyCarsExtended(IEnumerable<CompanyCarModel> allCompanyCars)
        {
            Log.Debug("Checking for any car in the database..");
            if (!_DAL.CompanyCarsIsEmpty())
            {
                List<CompanyCarModel> companyCarList = allCompanyCars.ToList();
                List<DGTCarModel> dgtCarList = new List<DGTCarModel>();
                Log.Debug("Redis data found");
                foreach (CompanyCarModel item in companyCarList)
                {
                    var extendedCarData = await _DAL.GetDGTCar(item.VIN);
                    if (extendedCarData.HasValue)
                    {
                        var dgtCar = JsonSerializer.Deserialize<DGTCar>(extendedCarData);
                        var dgtCarModeled = _mapper.Map<DGTCarModel>(dgtCar);
                        dgtCarList.Add(dgtCarModeled);
                    }
                }
                return dgtCarList.AsEnumerable();
            }
            Log.Debug("There is no cars in the database, returning empty.");
            return null;
        }

        public async Task AddCompanyCar(CompanyCarModel car)
        {
            var extendedCarData = await _DAL.GetDGTCar(car.VIN);
            Log.Debug("Checking if car exist in DTG DB..");
            if (extendedCarData.HasValue)
            {
                Log.Debug("Checking if car is already registered..");
                if (!_DAL.CompanyCarExist(car.VIN))
                {
                    Log.Debug("Adding..");
                    CompanyCar carToAdd = _mapper.Map<CompanyCar>(car);
                    _DAL.AddCompanyCar(carToAdd);
                }
                else
                    throw new InvalidOperationException($"Car {car.VIN} already in de DB.");
            }
            else
                throw new KeyNotFoundException($"Car {car.VIN} doesn´t exist in the Redis DB.");
        }

        public void UpdateCompanyCar(CompanyCarModel car)
        {
            Log.Debug("Checking if car exist in DB..");
            if (_DAL.CompanyCarExist(car.VIN))
            {
                Log.Debug("Updating");
                var carToUpdate = _mapper.Map<CompanyCar>(car);
                _DAL.UpdateCompanyCar(carToUpdate);
            }
            else
                throw new KeyNotFoundException($"Car {car.VIN} doesn´t exist in the DB.");

        }

        //public async Task<IEnumerable<DGTCarModel>> GetAllDTGCars()
        //{
        //if (!_DAL.CompanyCarsIsEmpty())
        //{
        //    List<CompanyCarModel> companyCarList = allCompanyCars.ToList();
        //    List<DGTCarModel> dgtCarList = new List<DGTCarModel>();
        //    foreach (CompanyCarModel item in companyCarList)
        //    {
        //        var extendedCarData = await _DAL.GetDGTCar(item.VIN);
        //        if (extendedCarData.HasValue)
        //        {
        //            var dgtCar = JsonSerializer.Deserialize<DGTCar>(extendedCarData);
        //            var dgtCarModeled = _mapper.Map<DGTCarModel>(dgtCar);
        //            dgtCarList.Add(dgtCarModeled);
        //        }
        //    }

        //    return dgtCarList.AsEnumerable();
        //}
        //    return null;
        //}

        public void DeleteCompanyCarById(string id)
        {
            Log.Debug("Checking if car exist in DB..");
            if (_DAL.CompanyCarExist(id))
            {
                Log.Debug("Removing");
                _DAL.DeleteCompanyCar(id);
            }
            else
                throw new KeyNotFoundException($"Car {id} doesn´t exist in the DB.");

        }
    }
}
