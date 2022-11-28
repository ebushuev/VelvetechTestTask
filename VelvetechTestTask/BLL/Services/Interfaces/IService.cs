using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IService<T>
    {
        Task<T> CreateAsync(T entity);

        Task<T> GetAsync(int id);

        Task<List<T>> GetAllAsync();

        Task DeleteByIdAsync(int id);

        Task<T> UpdateAsync(T entity);
    }
}
