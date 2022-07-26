using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.DataAccess.Data;
using Todo.DataAccess.Repository.IRepository;
using Todo.Domain.Entities;

namespace Todo.DataAccess.Repository
{
    public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        private readonly TodoContext todoContext;

        public TodoItemRepository(TodoContext todoContext) : base(todoContext)
        {
            this.todoContext = todoContext;
        }

        public async Task UpdateAsync(long id, TodoItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

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