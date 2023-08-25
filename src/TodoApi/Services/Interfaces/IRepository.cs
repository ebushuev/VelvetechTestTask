using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetItems();
        Task<T> GetItem(long id);
        Task UpdateItem(T entity);
        Task CreateItem(T entity);
        Task DeleteTodoItem(long id);
        Task<bool> IEntityExists(long id);
    }
}