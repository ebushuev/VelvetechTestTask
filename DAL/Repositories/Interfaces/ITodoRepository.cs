using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace DAL.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        public Task<IEnumerable<TodoItem>> GetAllAsync();
        public Task<TodoItem> GetByIdAsync(long id);
        public void Add(TodoItem todoItem);
        public void Remove(TodoItem todoItem);
        public Task<bool> AnyAsync(long id);
        public Task SaveChangesAsync();
    }
}