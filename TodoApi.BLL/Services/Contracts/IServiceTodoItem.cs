using TodoApi.BLL.Models;

namespace TodoApi.BLL.Services.Contracts;

public interface IServiceTodoItem
{
    public Task<IEnumerable<TodoItemDto>> GetTodoItems();

    public Task<TodoItemDto?> GetTodoItemById(long id);

    public Task<bool> UpdateTodoItem(TodoItemDto todoItemDto);

    public Task<TodoItemDto> CreateTodoItem(TodoItemDto todoItemDto);

    public Task<bool> DeleteTodoItem(long id);
}