using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Domain
{
    public interface IDataService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<T> UpdateAsync(T entity);
        Task<T> AddAsync(T entity);
        Task<bool> IsEntityExist(Guid id);
    }
}