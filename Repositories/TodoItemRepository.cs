using TodoData.Data;
using TodoEntities.DbSet;
using TodoIData.IRepositiries;

namespace TodoApiDTO.Repositories
{
    public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
    {
        private readonly TodoContext _context;
        public TodoItemRepository(TodoContext options) : base(options)
        {
            _context = options;
        }
    }
}
