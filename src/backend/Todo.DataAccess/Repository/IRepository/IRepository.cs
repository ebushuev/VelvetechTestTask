using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(long id);
        Task<long> CreateAsync(T item);
        Task DeleteAsync(long id);
    }
}
