using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Repositories
{
    public class CarRepository : ICommonActions<Car>
    {
        readonly CarMarketContext _db;

        public CarRepository(CarMarketContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Car obj)
        {
            await _db.Cars.AddAsync(obj);
            try
            {
                await _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                var m = ex;
            }
        }

        public async Task AddRangeAsync(IEnumerable<Car> objEnum)
        {
            await _db.Cars.AddRangeAsync(objEnum);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var obj = _db.Cars.Where(x => x.Id == id).FirstOrDefault();
            if (obj != null)
            {
                _db.Cars.Remove(obj);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Car>> GetAllAsync() => await _db.Cars.ToListAsync();

        public async Task<Car> GetByIdAsync(int id) => await _db.Cars.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task ModifyAsync(int id, Car obj)
        {
            if (obj.Id == id)
            {
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
        }
    }
}
