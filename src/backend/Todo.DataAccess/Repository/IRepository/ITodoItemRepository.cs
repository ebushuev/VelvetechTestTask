using System.Threading.Tasks;
using Todo.Domain.Entities;

namespace Todo.DataAccess.Repository.IRepository
{
    public interface ITodoItemRepository : IRepository<TodoItem>
    {
        public Task UpdateAsync(long id, TodoItem item);
    }
}
