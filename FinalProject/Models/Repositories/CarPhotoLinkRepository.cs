using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Repositories
{
    public class CarPhotoLinkRepository : ICommonActions<CarPhotoLink>
    {
        readonly CarMarketContext _db;

        public CarPhotoLinkRepository(CarMarketContext db)
        {
            _db = db;
        }

        public async Task AddAsync(CarPhotoLink obj)
        {
            await _db.CarPhotoLinks.AddAsync(obj);
            await _db.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<CarPhotoLink> objEnum)
        {
            await _db.CarPhotoLinks.AddRangeAsync(objEnum);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var obj = _db.CarPhotoLinks.Where(x => x.Id == id).FirstOrDefault();
            if (obj != null)
            {
                _db.CarPhotoLinks.Remove(obj);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<CarPhotoLink>> GetAllAsync() => await _db.CarPhotoLinks.ToListAsync();
        
        public async Task<CarPhotoLink> GetByIdAsync(int id) =>  await _db.CarPhotoLinks.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task ModifyAsync(int id, CarPhotoLink obj)
        {
            if (obj.Id == id)
            {
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();
            }
        }
    }
}
