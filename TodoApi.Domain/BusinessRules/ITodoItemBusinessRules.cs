using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.BusinessRules
{
    public interface ITodoItemBusinessRules
    {
        Task<TodoItem> GetByToDoItemId (IQueryable<TodoItem> query, long id, CancellationToken cancellationToken);

        Task<bool> TodoItemExists(IQueryable<TodoItem> query, long id, CancellationToken cancellationToken);
    }
}
