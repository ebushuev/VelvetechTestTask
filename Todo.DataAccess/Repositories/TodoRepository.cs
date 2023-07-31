using Microsoft.EntityFrameworkCore;
using Todo.Core.Business.TodoItem.Entities;
using Todo.Core.Business.TodoItem.Interfaces;

namespace Todo.DataAccess.Repositories
{
    public abstract class TodoRepository : BaseRepository<TodoItem, Guid>, ITodoRepository
    {
        protected TodoRepository(DbContext dbContext): base(dbContext)
        {
        }
    }
}
