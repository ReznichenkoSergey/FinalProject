using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.Interfaces
{
    public interface ICommonActions<T> where T:class, new()
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T obj);
        Task AddRangeAsync(IEnumerable<T> objEnum);
        Task ModifyAsync(int id, T obj);
        Task DeleteAsync(int id);
    }
}
