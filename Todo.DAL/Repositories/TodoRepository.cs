using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.DAL.Contexts;
using TodoApi.DAL.Models;

namespace Todo.DAL.Repositories
{
    internal class TodoRepository : ITodoRepository
    {
        private readonly TodoContext todoContext;

        public TodoRepository(TodoContext todoContext)
        {
            this.todoContext = todoContext;
        }
        public async Task<long> CreateAsync(TodoItem item)
        {
            todoContext.TodoItems.Add(item);
            await todoContext.SaveChangesAsync();
            return item.Id;
        }

        public async Task DeleteAsync(long id)
        {
            var todoItem = await todoContext.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                throw new KeyNotFoundException();
            }
            todoContext.TodoItems.Remove(todoItem);
            await todoContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetAsync()
        {
            return await todoContext.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetAsync(long id)
        {
            return await todoContext.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(long id, TodoItem item)
        {
            var todoItem = await todoContext.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                throw new KeyNotFoundException();
            }
            todoItem.Name = item.Name;
            todoItem.IsComplete = item.IsComplete;
            todoItem.Secret = item.Secret;
            await todoContext.SaveChangesAsync();
        }
    }
}
