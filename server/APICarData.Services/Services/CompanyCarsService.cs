
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using APICarData.Domain.Models;
using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces.CompanyCars;
using System.Text.Json;
using System.Linq;

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
            if (!_DAL.CompanyCarsIsEmpty())
            {
                var allCars = await _DAL.GetAllCompanyCars();
                return _mapper.Map<IEnumerable<CompanyCarModel>>(allCars);
            }
            return null;
        }

        public async Task<IEnumerable<DGTCarModel>> GetAllCompanyCarsExtended(IEnumerable<CompanyCarModel> allCompanyCars)
        {
            if (!_DAL.CompanyCarsIsEmpty())
            {
                List<CompanyCarModel> companyCarList = allCompanyCars.ToList();
                List<DGTCarModel> dgtCarList = new List<DGTCarModel>();
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
            return null;
        }

        public async Task<bool> AddCompanyCar(CompanyCarModel car)
        {
            var extendedCarData = await _DAL.GetDGTCar(car.VIN);
            if (extendedCarData.HasValue)
            {
                if (!_DAL.CompanyCarExist(car.VIN))
                {
                    CompanyCar carToAdd = _mapper.Map<CompanyCar>(car);
                    _DAL.AddCompanyCar(carToAdd);
                    return true;
                }
            }
            return false;
        }

        public bool UpdateCompanyCar(CompanyCarModel car)
        {
            if (_DAL.CompanyCarExist(car.VIN))
            {
                var carToUpdate = _mapper.Map<CompanyCar>(car);
                _DAL.UpdateCompanyCar(carToUpdate);
                return true;
            }
            return false;
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

        public bool DeleteCompanyCarById(string id)
        {
            if (_DAL.CompanyCarExist(id))
            {
                _DAL.DeleteCompanyCar(id);
                return true;
            }
            return false;
        }
    }
}
