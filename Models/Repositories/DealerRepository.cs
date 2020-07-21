using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Repositories
{
    public class DealerRepository : ICommonActions<Dealer>
    {
        readonly CarMarketContext _db;

        public DealerRepository(CarMarketContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Dealer obj)
        {
            var temp = _db.Dealers.Where(x => x.NativeId == obj.NativeId && obj.NativeId > 0).FirstOrDefault();
            if (temp == null)
            {
                await _db.Dealers.AddAsync(obj);
                await _db.SaveChangesAsync();
            }
        }

        public async Task AddRangeAsync(IEnumerable<Dealer> objEnum)
        {
            await _db.Dealers.AddRangeAsync(objEnum);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var obj = _db.Dealers.Where(x => x.Id == id).FirstOrDefault();
            if (obj != null)
            {
                _db.Dealers.Remove(obj);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Dealer>> GetAllAsync() => await _db.Dealers.ToListAsync();

        public async Task<Dealer> GetByIdAsync(int id) => await _db.Dealers.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task ModifyAsync(int id, Dealer obj)
        {
            if (obj.Id == id)
            {
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
        }
    }
}
