using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Velvetech.Todo.Logic.Models;
using Velvetech.Todo.Repositories.Entities;
using Velvetech.Todo.Repositories.Interfaces;

namespace Velvetech.Todo.Logic
{
  public class TodoItemService : ITodoItemService
  {
    private readonly IDbTodoItemsRepository _repo;
    private readonly IMapper _mapper;

    public TodoItemService(IDbTodoItemsRepository repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }

    public async Task<TodoItemModel> InsertTodoItemAsync(TodoItemModel item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof(item));

      var itemToInsert = _mapper.Map<DbTodoItem>(item);

      var insertedItem = await _repo.InsertTodoItemAsync(itemToInsert);

      return _mapper.Map<TodoItemModel>(insertedItem);
    }

    public async Task DeleteTodoItemAsync(long id)
    {
      await GetDbTodoItemByIdAsync(id);

      await _repo.DeleteTodoItemAsync(id);
    }

    public async Task<IEnumerable<TodoItemModel>> GetAllTodoItemsAsync()
    {
      var todoItems = await _repo.GetAllTodoItemsAsync();

      return _mapper.Map<IEnumerable<TodoItemModel>>(todoItems);
    }

    public async Task<TodoItemModel> GetTodoItemByIdAsync(long id)
    {
      var item = await GetDbTodoItemByIdAsync(id);

      return _mapper.Map<TodoItemModel>(item);
    }

    public async Task<TodoItemModel> UpdateTodoItemAsync(TodoItemModel item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof(item));

      await GetDbTodoItemByIdAsync(item.Id);

      var updatedItem = await _repo.UpdateTodoItemAsync(_mapper.Map<DbTodoItem>(item));

      return _mapper.Map<TodoItemModel>(updatedItem);
    }

    private async Task<DbTodoItem> GetDbTodoItemByIdAsync(long id)
    {
      var item = await _repo.GetTodoItemByIdAsync(id);

      if (item == null)
        throw new ArgumentException($"Todo Item with Id {id} is not found.");

      return item;
    }
  }
}
