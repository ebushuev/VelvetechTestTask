using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using TodoApi.DAL.Models;

namespace DAL.Repositories
{
    public interface ITodoRepository
    {
        Task<TodoItem> GetAsync(long id, CancellationToken token);
        Task<List<TodoItem>> GetAsync(CancellationToken token);
        Task DeleteAsync(long id, CancellationToken token);
        Task<TodoItem> CreateAsync(TodoItem item, CancellationToken token);
        Task<TodoItem> UpdateAsync(TodoItem item, CancellationToken token);
    }
}
