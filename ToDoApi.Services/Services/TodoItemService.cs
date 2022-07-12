using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Data.Interfaces;
using TodoApi.Data.Models;
using TodoApi.Services.Exceptions;
using TodoApi.Services.Extensions;
using TodoApi.Services.Models;
using TodoApi.Services.Services.Interfaces;

namespace TodoApi.Services.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly IRepository<TodoItem, long> _repository;
        public TodoItemService(IRepository<TodoItem, long> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<TodoItem>> GetAsync()
        {
            return await _repository.GetAsync();
        }

        public async Task<TodoItem> GetAsync(long id)
        {
            Argument.Id(id);

            return await _repository.GetAsync(id);
        }

        public async Task<TodoItem> CreateAsync(TodoItemDTO item)
        {
            Argument.NotNull(item);

            var newItem = new TodoItem()
            {
                IsComplete = item.IsComplete,
                Name = item.Name,
                Secret = item.GetHashCode().ToString(),
            };

            _repository.Create(newItem);
            await _repository.SaveAsync();
            return newItem;
        }

        public async Task<TodoItem> UpdateAsync(long id, TodoItemDTO item)
        {
            Argument.Id(id);
            Argument.NotNull(item);

            var existedItem = await GetAsync(id);

            if (existedItem == null)
                throw new NotFoundException(nameof(TodoItem), id);

            existedItem.Name = item.Name;
            existedItem.IsComplete = item.IsComplete;

            _repository.Update(existedItem);
            await _repository.SaveAsync();
            return existedItem;
        }

        public async Task DeleteAsync(long id)
        {
            Argument.Id(id);
            var existedItem = await GetAsync(id);

            if (existedItem == null)
                throw new NotFoundException(nameof(TodoItem), id);

            _repository.Delete(existedItem);
            await _repository.SaveAsync();
        }
    }
}
