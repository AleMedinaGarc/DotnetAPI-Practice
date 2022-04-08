
using APICarData.DataAccessLayer.Data.ApiContext;
using APICarData.DataAccessLayer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICarData.DataAccessLayer
{
    public class CompanyCarsDAL
    {
        private readonly ApiContext _context;
        public CompanyCarsDAL(ApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CompanyCar>> GetAllCompanyCars()
        {
            try
            {
                var cars = await _context.CompanyCars
                    .OrderBy(x => x.numberPlate)
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

        public void AddCompanyCar(CompanyCar car) //maybe async
        {   //DGTCar carRedis = _context.redis.FirstOrDefault(p => p.VIN == car.VIN); // busca un coche en redis con VIN
            //reflection car DGTCar
            //_context.CompanyCars.Add(car);
            _context.SaveChanges();
        }

        public void UpdateCompanyCar(CompanyCar car, string id)
        {
            try
            {
                if (car.VIN == id)
                {
                    _context.Entry(car).State = EntityState.Modified;
                    _context.SaveChanges();
                }
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
            var car = _context.CompanyCars.FirstOrDefault(p => p.VIN == id);
            _context.CompanyCars.Remove(car);
            _context.SaveChanges();
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

        public bool DGTCarsAny()
        {
            //return _context.CompanyCars.Any();
            return true;
        }

        public bool CompanyCarsIsEmpty()
        {
            if (_context.CompanyCars.Any())
                return false;
            return true;
        }


    }
}
