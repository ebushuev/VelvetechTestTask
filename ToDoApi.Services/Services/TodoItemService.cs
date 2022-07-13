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
        private readonly ITodoItemMappingService _mappingService;

        public TodoItemService(IRepository<TodoItem, long> repository, ITodoItemMappingService mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAsync()
        {
            var result = await _repository.GetAsync();
            return await Task.FromResult(_mappingService.MapTodoItemToDTO(result));
        }

        protected internal async Task<TodoItem> GetByIdAsync(long id)
        {
            Argument.Id(id);

            return await _repository.GetAsync(id);
        }

        public async Task<TodoItemDTO> GetAsync(long id)
        {
            Argument.Id(id);

            var result = await GetByIdAsync(id);
            if (result == null)
                throw new NotFoundException(nameof(TodoItem), id);

            return await Task.FromResult(_mappingService.MapTodoItemToDTO(result));
        }

        public async Task<TodoItemDTO> CreateAsync(TodoItemDTO item)
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

            return await Task.FromResult(_mappingService.MapTodoItemToDTO(newItem));
        }

    public async Task UpdateAsync(long id, TodoItemDTO item)
        {
            Argument.Id(id);
            Argument.NotNull(item);

            var existedItem = await GetByIdAsync(id);

            if (existedItem == null)
                throw new NotFoundException(nameof(TodoItem), id);

            existedItem.Name = item.Name;
            existedItem.IsComplete = item.IsComplete;

            _repository.Update(existedItem);
            await _repository.SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            Argument.Id(id);
            var existedItem = await GetByIdAsync(id);

            if (existedItem == null)
                throw new NotFoundException(nameof(TodoItem), id);

            _repository.Delete(existedItem);
            await _repository.SaveAsync();
        }
    }
}
