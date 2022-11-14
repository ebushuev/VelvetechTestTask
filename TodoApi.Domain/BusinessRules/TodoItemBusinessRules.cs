using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.BusinessRules
{
    public class TodoItemBusinessRules : ITodoItemBusinessRules
    {
        public async Task<TodoItem> GetByToDoItemId(IQueryable<TodoItem> query, long id, CancellationToken cancellationToken)
        {
            return await query.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }


        public async Task<bool> TodoItemExists(IQueryable<TodoItem> query, long id, CancellationToken cancellationToken) =>
            await query.AnyAsync(e => e.Id == id, cancellationToken: cancellationToken);
    }
}
