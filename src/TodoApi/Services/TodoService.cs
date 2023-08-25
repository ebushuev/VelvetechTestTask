using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Services.Interfaces;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly IRepository<TodoItem> _repository;

        public TodoService(IRepository<TodoItem> repository)
        {
            _repository = repository;
        }

        public Task CreateTodoItem(TodoItemDTO Item)
        {
            var todoItem = Item.AsEntity();
            return _repository.CreateItem(todoItem);
        }

        public async Task DeleteTodoItem(long id)
            => await _repository.DeleteTodoItem(id);

        public async Task<TodoItem> GetTodoItem(long id)
            => await _repository.GetItem(id);

        public async Task<IEnumerable<TodoItem>> GetTodoItems()
            => await _repository.GetItems();

        public  async Task UpdateTodoItem(long id, TodoItemDTO Item)
        {
            var todoItem = await GetTodoItem(id);
            todoItem.Name = Item.Name;
            todoItem.IsComplete = Item.IsComplete;
            await _repository.UpdateItem(todoItem);
        }
        public Task<bool> TodoItemExists(long id)
            => _repository.IEntityExists(id);
    }

   
}
