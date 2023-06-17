using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApi.BLL.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(long id);
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
