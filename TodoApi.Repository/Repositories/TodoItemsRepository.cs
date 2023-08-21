namespace TodoApi.Repository.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TodoApi.ObjectModel.Contracts.Repositories;
    using TodoApi.ObjectModel.Models;
    using TodoApi.ObjectModel.Models.Exceptions;
    using TodoApi.Repository;

    internal sealed class TodoItemsRepository : ITodoItemsRepository
    {
        private readonly TodoContext _context;

        public TodoItemsRepository(TodoContext context)
        {
            _context = context;
        }

        public void Add(TodoItem todoItem)
            => _context.TodoItems.Add(todoItem);

        public async Task<TodoItem> FindAsync(long id, CancellationToken cancellationToken)
        {
            return await _context.TodoItems
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<TodoItem>> GetItemsAsync(CancellationToken cancellationToken)
        {
            return await _context.TodoItems
                .ToListAsync(cancellationToken);
        }

        public void Delete(TodoItem item)
            => _context.TodoItems.Remove(item);
    }
}