using DataAccessLayer.Repository.Concrete;
using Velvetech.TodoApp.Domain.Entities;
using Velvetech.TodoApp.Infrastructure.Data;
using Velvetech.TodoApp.Infrastructure.Repositories.Abstractions.Custom;

namespace Velvetech.TodoApp.Infrastructure.Repositories.Implementations.Custom
{
    public class TodoItemRepository : BaseRepository<TodoItemEntity>, ITodoItemRepository
    {
        public TodoItemRepository(TodoContext dbContext) : base(dbContext)
        {
        }
    }
}
