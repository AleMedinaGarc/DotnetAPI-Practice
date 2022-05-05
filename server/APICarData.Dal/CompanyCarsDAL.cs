using APICarData.Domain.Data;
using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces;
using APICarData.Domain.Interfaces.CompanyCars;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICarData.Dal
{
    public class CompanyCarsDAL : ICompanyCarsDAL
    {
        private readonly IApiContext _context;
        private readonly IConnectionMultiplexer _redis;
        public CompanyCarsDAL(IConnectionMultiplexer redis, IApiContext context)
        {
            _context = context;
            _redis = redis;
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

        public async Task<RedisValue> GetDGTCar(string id)
        {
            try
            {
                return await _redis.GetDatabase().StringGetAsync(id);
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }

        //public async Task<List<RedisValue>> GetAllDGTCar()
        //{
        //    try
        //    {
                //var keys = _redis.SearchKeys("*");
                //List<string> keyList = new List<string>();
                //List<RedisValue> carList = new List<RedisValue>();
                //keyList = server.Keys(_dataCache.Database);
                //// redisvaluelist
                //// listkeys= await allrediskeys
                //foreach (var key in keyList)
                //{
                //    carList.Add(await _redis.GetDatabase().StringGetAsync(key));
                //}
                //   add to redis value list
                // return RedisValue value list
                //return carList;
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Source != null)
        //            Console.WriteLine("Exception source:", ex.Source);
        //        throw;
        //    }
        //}

        public void AddCompanyCar(CompanyCar car)
        {
            try
            {
                _context.Insert(car);
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
                    _context.UpdateEntry(car);
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
                _context.Delete(car);
            }
            catch (Exception ex)
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
                return !_context.CompanyCars.Any();
            }
            catch (ArgumentNullException ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }
        public bool CompanyCarExist(string id)
        {
            try
            {
                return _context.CompanyCars.Any(p => p.VIN == id);
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("Exception source:", ex.Source);
                throw;
            }
        }
    }
}
