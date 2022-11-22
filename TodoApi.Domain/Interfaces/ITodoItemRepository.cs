using TodoApi.Domain.Models;

namespace TodoApi.Domain.Interfaces
{
    public interface ITodoItemRepository : IRepository<TodoItem, long> { }
}
