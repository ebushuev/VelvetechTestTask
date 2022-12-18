using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> List();
        Task<T> FindAsync(long id);
        Task<T> Create(T item);
        Task<bool> Remove(long id);
        Task<bool> Update(T item);
    }
}
