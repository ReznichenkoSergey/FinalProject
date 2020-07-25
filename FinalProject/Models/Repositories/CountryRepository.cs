using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Repositories
{
    public class CountryRepository : ICommonActions<Country>
    {
        readonly CarMarketContext _db;

        public CountryRepository(CarMarketContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Country obj)
        {
            await _db.Countries.AddAsync(obj);
            await _db.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Country> objEnum)
        {
            await _db.Countries.AddRangeAsync(objEnum);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var obj = _db.Countries.Where(x => x.Id == id).FirstOrDefault();
            if (obj != null)
            {
                _db.Countries.Remove(obj);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Country>> GetAllAsync() => await _db.Countries.ToListAsync();
        
        public async Task<Country> GetByIdAsync(int id) =>  await _db.Countries.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task ModifyAsync(int id, Country obj)
        {
            if (obj.Id == id)
            {
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
        }
    }
}
