using APICarData.Domain.Data;
using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces;
using APICarData.Domain.Interfaces.CompanyCars;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICarData.Dal
{
    public class CompanyCarsDAL : ICompanyCarsDAL
    {
        private readonly IApiContext _context;
        public CompanyCarsDAL(/*IConnectionMultiplexer redis, */IApiContext context)
        {
            _context = context;
            // _redis = redis;
        }

        public async Task<IEnumerable<CompanyCar>> GetAllCompanyCars()
        {
            try
            {
                IEnumerable<CompanyCar> cars = await _context.CompanyCars
                    .OrderBy(x => x.NumberPlate)
                    .ToListAsync();
                return cars;
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }

        //public async Task GetRedisCarById(string id)
        //{
        //    return await _redis.GetDatabase().StringGetAsync(id);
        //}

        public void AddCompanyCar(CompanyCar car)
        {
            try
            {
                _context.InsertCar(car);
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }

        public void UpdateCompanyCar(CompanyCar car)
        {
            try
            {
                    _context.UpdateCar(car);
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }

        }

        public void DeleteCompanyCar(string id)
        {
            try
            {
                var car = _context.CompanyCars.FirstOrDefault(p => p.VIN == id);
                _context.DeleteCar(car);
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }

        public bool CompanyCarsAny()
        {
            try
            {
                return _context.CompanyCars.Any();
            }
            catch (ArgumentNullException ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }

        public bool CompanyCarsIsEmpty()
        {
            try
            {
                return _context.CompanyCars.Any();
            }
            catch (ArgumentNullException ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }
    }
}
