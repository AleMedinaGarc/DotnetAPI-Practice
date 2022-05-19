using APICarData.Domain.Data.Entities;
using APICarData.Domain.Interfaces;
using APICarData.Domain.Interfaces.CompanyCars;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
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
            IEnumerable<CompanyCar> cars = await _context.CompanyCars
                .OrderBy(x => x.NumberPlate)
                .ToListAsync();
            return cars;
        }

        public async Task<RedisValue> GetDGTCar(string id)
        {
            return await _redis.GetDatabase().StringGetAsync(id);
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
            _context.Insert(car);
        }

        public void UpdateCompanyCar(CompanyCar car)
        {
            _context.UpdateEntry(car);
        }

        public void DeleteCompanyCar(string id)
        {
            var car = _context.CompanyCars.FirstOrDefault(p => p.VIN == id);
            _context.Delete(car);

        }

        public bool CompanyCarsIsEmpty()
        {
            return !_context.CompanyCars.Any();

        }
        public bool CompanyCarExist(string id)
        {
            return _context.CompanyCars.Any(p => p.VIN == id);
        }
    }
}
