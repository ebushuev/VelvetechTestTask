using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain.Entities;

namespace Todo.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetAsync();
        Task<TodoItem> GetAsync(long id);
        Task<long> CreateAsync(TodoItem item);
        Task UpdateAsync(long id, TodoItem item);
        Task DeleteAsync(long id);
    }
}