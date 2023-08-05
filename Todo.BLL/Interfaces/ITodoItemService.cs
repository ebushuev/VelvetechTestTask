using Todo.DAL.Entities;

namespace Todo.BLL.Interfaces;

public interface ITodoItemService
{
    Task<IEnumerable<TodoItem>> GetTodoItemsAsync(bool trackChanges);

    Task<TodoItem> GetTodoItemAsync(Guid todoItemId, bool trackChanges);

    Task CreateToDoItemAsync(TodoItem todoItem);

    Task UpdateTodoItemAsync(TodoItem todoItem);

    Task DeleteTodoItemAsync(TodoItem todoItem);
}
