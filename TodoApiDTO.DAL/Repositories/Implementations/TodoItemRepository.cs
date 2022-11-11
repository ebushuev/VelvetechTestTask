using System.Linq;
using TodoApiDTO.DAL.Contexts;
using TodoApiDTO.DAL.Entities;
using TodoApiDTO.DAL.Repositories.Abstractions;

namespace TodoApiDTO.DAL.Repositories.Implementations
{
    public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }

        public bool TodoItemExists(long id) => _dbSet.Any(t => t.Id == id);
    }
}
