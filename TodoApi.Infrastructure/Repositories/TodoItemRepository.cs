using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Models;
using TodoApi.Models;

namespace TodoApi.Infrastructure.Repositories
{
    public class TodoItemRepository : RepositoryBase<TodoItem, long>, ITodoItemRepository
    {
        public TodoItemRepository(TodoContext context) : base(context) { }
    }
}
