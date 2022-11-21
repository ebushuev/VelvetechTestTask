using TodoApiDTO.Data.Models;

namespace TodoApiDTO.Data.Repositories;

public interface ITodoItemRepository
{
    public Task<List<TodoItem>> GetAll();
    public Task<TodoItem?> Get(long id);
    public Task<bool> Exists(long id);
    public void Remove(TodoItem item);
    public Task Add(TodoItem todoItem);
}