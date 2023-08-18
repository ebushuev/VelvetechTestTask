using Todo.Core.Interfaces;
using Todo.Infrastructure.Entities;
using Todo.Infrastructure.Interfaces;

namespace Todo.Core.Services;

public class ItemService : IItemService
{
    private readonly IRepository<TodoItem> _repository;

    public ItemService(IRepository<TodoItem> repository)
    {
        _repository = repository;
    }

    public async Task Create(TodoItem item)
    {
        await _repository.Create(item);
    }

    public async Task<TodoItem> Read(long id)
    {
        return await _repository.Read(x => x.Id == id);
    }

    public async Task Update(TodoItem item)
    {
        await _repository.Update(item);
    }

    public async Task Delete(TodoItem item)
    {
        await _repository.Delete(item);
    }

    public async Task<IEnumerable<TodoItem>> GetAll()
    {
        return await _repository.GetAll();
    }
}