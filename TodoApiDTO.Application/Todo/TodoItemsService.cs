using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TodoApiDTO.Domain.Todo;
using TodoApiDTO.Infrastructure.Services.Todo;

namespace TodoApiDTO.Application.Todo
{
    public class TodoItemsService : ITodoItemsService
    {
        private readonly ILogger<TodoItemsService> _logger;
        private readonly ITodoItemsRepository _todoItemsRepository;

        public TodoItemsService(
            ILogger<TodoItemsService> logger,
            ITodoItemsRepository todoItemsRepository) 
        {
            _logger = logger;
            _todoItemsRepository = todoItemsRepository;
        }

        public async Task<IEnumerable<TodoItemDto>> GetItems()
        {
            _logger.LogDebug($"{nameof(TodoItemsService)}.{nameof(GetItems)} Start.");

            try
            {
                var items = await _todoItemsRepository.GetItems();
                _logger.LogDebug($"{nameof(TodoItemsService)}.{nameof(GetItems)} {items.Count()} records received from the database");

                var result = items.AsQueryable().Select(TodoItemDto.Expression).ToList();

                _logger.LogDebug($"{nameof(TodoItemsService)}.{nameof(GetItems)} End. Count: {result.Count()}");
                return result;
            }
            catch ( Exception exc ) 
            {
                _logger.LogError(exc, $"Error get items.");
                throw;
            }
        }

        public async Task<TodoItemDto> GetItem(long id)
        {
            _logger.LogDebug($"{nameof(TodoItemsService)}.{nameof(GetItem)} Start.");

            if (id < 1)
            {
                throw new ArgumentException(nameof(id));
            }

            try
            {
                var item = await _todoItemsRepository.GetItem(id);
                _logger.LogDebug($"{nameof(TodoItemsService)}.{nameof(GetItem)} End. IsEmpty: {item == null}");
                return item == null ? null : TodoItemDto.GetDto(item);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, $"Error Get item. Id: {id}");
                throw;
            }
        }

        public async Task Create(string name, bool isComplete)
        {
            _logger.LogDebug($"{nameof(TodoItemsService)}.{nameof(Create)} Start.");

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            try
            {
                var newId = await _todoItemsRepository.CreateItem(name, isComplete);
                _logger.LogDebug($"{nameof(TodoItemsService)}.{nameof(Create)} End. Id: {newId}.");
            }
            catch(Exception exc) 
            {
                _logger.LogError(exc, $"Error Create. Name: {name}, IsComplete: {isComplete}");
                throw;
            }
        }

        public async Task UpdateItem(long id, string name, bool? isComplete)
        {
            _logger.LogDebug($"{nameof(TodoItemsService)}.{nameof(UpdateItem)} Start.");

            if (id < 1)
            {
                throw new ArgumentException(nameof(id));
            }

            try
            {
                var item = await _todoItemsRepository.GetItem(id);

                if (item != null)
                {
                    var isUpdate = false;
                    var newName = item.Name;
                    var newComplete = item.IsComplete;

                    if (!string.IsNullOrWhiteSpace(name) && !string.Equals(item.Name, name))
                    {
                        newName = name;
                        isUpdate = true;
                    }

                    if (isComplete.HasValue && item.IsComplete != isComplete.Value)
                    {
                        newComplete = isComplete.Value;
                        isUpdate = true;
                    }

                    if (isUpdate)
                    {
                        var upItem = new TodoItem(id, newName, newComplete);
                        await _todoItemsRepository.UpdateItem(upItem);
                    }
                }
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, $"Error Update. Id: {id}");
                throw;
            }

            _logger.LogDebug($"{nameof(TodoItemsService)}.{nameof(UpdateItem)} End.");
        }

        public async Task DeleteItem(long id)
        {
            _logger.LogDebug($"{nameof(TodoItemsService)}.{nameof(DeleteItem)} Start.");

            if (id < 1)
            {
                throw new ArgumentException(nameof(id));
            }

            try
            {
                var item = await _todoItemsRepository.GetItem(id);
                if (item != null)
                {
                    await _todoItemsRepository.DeleteItem(item);
                }
            }
            catch (Exception exc) 
            {
                _logger.LogError(exc, $"Error delete item. Id: {id}");
                throw;
            }

            _logger.LogDebug($"{nameof(TodoItemsService)}.{nameof(DeleteItem)} End.");
        }
    }
}
