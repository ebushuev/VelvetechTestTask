using Todo.Core.Business.TodoItem.Entities;
using Todo.Core.Business.TodoItem.Interfaces;

namespace Todo.DataAccess.Repositories
{
    public class TodoRepository : BaseRepository<TodoItem, Guid>, ITodoRepository
    {
        public TodoRepository(TodoContext todoContext): base(todoContext)
        {
        }
    }
}
