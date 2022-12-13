using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Data.Entities;
using TodoApp.Data.Repositories;

namespace TodoApp.Data.Todo.Repositories
{
    public interface ITodoRepository : IRepository<TodoItem>
    {
        bool HasItem(long id);
    }

    public class TodoRepository : Repository<TodoItem>, ITodoRepository
    {
        public TodoRepository(TodoContext context) : base(context)
        {
        }

        public bool HasItem(long id) => _context.TodoItems.Any(x => x.Id == id);
    }


}
