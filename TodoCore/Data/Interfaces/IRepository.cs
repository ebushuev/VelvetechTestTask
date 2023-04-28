using System.Collections.Generic;
using System.Threading.Tasks;
using TodoCore.Data.Common;

namespace TodoCore.Data.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task<bool> IsExistAsync(long id);
        void Add(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
