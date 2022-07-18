using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.DAL.Models;

namespace Todo.DAL.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAsync();
        Task<TodoItem> GetAsync(long id);
        Task<long> CreateAsync(TodoItem item);
        Task UpdateAsync(long id, TodoItem item);
        Task DeleteAsync(long id);
    }
}
