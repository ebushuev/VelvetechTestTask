using Todo.Infrastructure.Entities;

namespace Todo.Core.Interfaces;

public interface IItemService
{
    Task Create(TodoItem item);

    Task<TodoItem> Read(long id);

    Task Update(TodoItem item);

    Task Delete(TodoItem item);

    Task<IEnumerable<TodoItem>> GetAll();
}