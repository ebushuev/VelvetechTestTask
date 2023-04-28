using TodoCore.Data.Entities;
using TodoCore.Data.Interfaces;

namespace TodoInfrastructure.DataAccess.Repositories
{
    public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(ApplicationDbContext context) : base(context) { }
    }
}
