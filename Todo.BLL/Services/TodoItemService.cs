using Todo.DAL.Entities;
using Todo.DAL;
using Todo.BLL.Interfaces;

namespace Todo.BLL.Services;

public class TodoItemService : ITodoItemService
{
    private readonly IRepository<TodoItem> _repository;

    public TodoItemService(IRepository<TodoItem> repository) => _repository = repository;

    public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync(bool trackChanges)
    {
        return await _repository.GetAll(trackChanges);
    }

    public async Task<TodoItem> GetTodoItemAsync(Guid todoItemId, bool trackChanges)
    {
        return await _repository.GetByCondition(l => l.Id == todoItemId, trackChanges);
    }

    public async Task CreateToDoItemAsync(TodoItem todoItem)
    {
        _repository.Create(todoItem);

        await _repository.SaveChanges();
    }

    public async Task DeleteTodoItemAsync(TodoItem todoItem)
    {
        _repository.Delete(todoItem);

        await _repository.SaveChanges();
    }

    public async Task UpdateTodoItemAsync(TodoItem todoItem)
    {
        _repository.Update(todoItem);

        await _repository.SaveChanges();
    }
}
