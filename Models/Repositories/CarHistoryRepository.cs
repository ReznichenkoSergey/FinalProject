using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Repositories
{
    public class CarHistoryRepository : ICommonActions<CarHistory>
    {
        readonly CarMarketContext _db;

        public CarHistoryRepository(CarMarketContext db)
        {
            _db = db;
        }

        public async Task AddAsync(CarHistory obj)
        {
            await _db.CarHistories.AddAsync(obj);
            try
            {
                await _db.SaveChangesAsync();
            }catch(Exception ex)
            {
                var d = ex;
            }
        }

        public async Task AddRangeAsync(IEnumerable<CarHistory> objEnum)
        {
            await _db.CarHistories.AddRangeAsync(objEnum);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var obj = _db.CarHistories.Where(x => x.Id == id).FirstOrDefault();
            if (obj != null)
            {
                _db.CarHistories.Remove(obj);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<CarHistory>> GetAllAsync() => await _db.CarHistories.ToListAsync();

        public async Task<CarHistory> GetByIdAsync(int id) => await _db.CarHistories.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task ModifyAsync(int id, CarHistory obj)
        {
            if (obj.Id == id)
            {
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
        }
    }
}
