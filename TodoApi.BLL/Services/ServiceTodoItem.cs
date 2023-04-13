using AutoMapper;
using TodoApi.BLL.Models;
using TodoApi.BLL.Services.Contracts;
using TodoApi.DAL.Models;
using TodoApi.DAL.Repositories.Contracts;

namespace TodoApi.BLL.Services;

public class ServiceTodoItem : IServiceTodoItem
{
    private readonly IRepository<TodoItemEntity> _repository;
    private readonly IMapper _mapper;

    public ServiceTodoItem(IRepository<TodoItemEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TodoItemDto>> GetTodoItems()
    {
        var todoItems = await _repository.GetAll();

        return todoItems.Select(i => _mapper.Map<TodoItemDto>(i)).ToList();
    }
    
    public async Task<TodoItemDto?> GetTodoItemById(long id)
    {
        var todoItem = await _repository.GetById(id);

        return _mapper.Map<TodoItemDto>(todoItem);
    }
    
    public async Task<bool> UpdateTodoItem(TodoItemDto todoItemDto)
    {
        return await _repository.Update(_mapper.Map<TodoItemEntity>(todoItemDto));
    }
    
    public async Task<TodoItemDto> CreateTodoItem(TodoItemDto todoItemDto)
    {
        var createdItem = await _repository.Create(_mapper.Map<TodoItemEntity>(todoItemDto));

        return _mapper.Map<TodoItemDto>(createdItem);
    }
    
    public async Task<bool> DeleteTodoItem(long id)
    {
        return await _repository.Delete(id);
    }
}