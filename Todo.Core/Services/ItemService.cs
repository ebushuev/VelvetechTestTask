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
        _repository.Create(item);
        await _repository.SaveChanges();
    }

    public async Task<TodoItem> Read(long id)
    {
        return await _repository.Read(x => x.Id == id);
    }

    public async Task Update(TodoItem item)
    {
        _repository.Update(item);
        await _repository.SaveChanges();
    }

    public async Task Delete(TodoItem item)
    {
        _repository.Delete(item);
        await _repository.SaveChanges();
    }

    public async Task<IEnumerable<TodoItem>> GetAll()
    {
        return await _repository.GetAll();
    }
}