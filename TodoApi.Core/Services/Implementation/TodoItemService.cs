using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Common.Exceptions;
using TodoApi.Core.DTOs;
using TodoApi.Core.Mappers;
using TodoApi.Core.Requests;
using TodoApi.Core.Services.Contract;
using TodoApiRepository.Models;
using TodoApiRepository.Repositories.Contract;

namespace TodoApi.Core.Services.Implementation
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository) 
        {
            _todoItemRepository = todoItemRepository;
        }

        /// <inheritdoc/>
        public async Task<ICollection<TodoItemDTO>> GetPagedTodoItems(PagedTodoItemRequest pagedTodoItemRequest) 
        {
            return (await _todoItemRepository.GetPagedToDoItemsAsync(pageSize: pagedTodoItemRequest.PageSize,
                    pageNumber: pagedTodoItemRequest.PageNumber)
                    .ConfigureAwait(false))
                .Select(p => p.ItemToDTO())
                .ToArray();
        }

        /// <inheritdoc/>
        public async Task<TodoItemDTO> GetTodoItemById(long todoItemId) 
        {
            return (await GetTodoItemByIdOrThrow(todoItemId).ConfigureAwait(false))
                .ItemToDTO();
        }

        /// <inheritdoc/>
        public async Task<TodoItemDTO> AddTodoItem(TodoItemArgs todoItemArgs) 
        {
            var todoItem = todoItemArgs.ArgsToItem();
            
            await _todoItemRepository.AddTodoItemAsync(todoItem)
                .ConfigureAwait(false);
            
            return todoItem.ItemToDTO();
        }

        /// <inheritdoc/>
        public async Task UpdateTodoItem(long todoItemId, TodoItemArgs todoItemDto) 
        {
            var todoItem = await GetTodoItemByIdOrThrow(todoItemId)
                .ConfigureAwait(false);
            
            UpdateTodoItem(itemToUpdate: todoItem,  updateInformationTodoItem: todoItemDto);
            
            await _todoItemRepository.UpdateTododItem(todoItem)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task DeleteTodoItem(long id) 
        {
            var todoItem = await GetTodoItemByIdOrThrow(id);
            
            await _todoItemRepository.DeleteTodoItem(todoItem)
                .ConfigureAwait(false);
        }

        private void UpdateTodoItem(TodoItem itemToUpdate, TodoItemArgs updateInformationTodoItem) 
        {
            itemToUpdate.Name = updateInformationTodoItem.Name;
            itemToUpdate.IsComplete = updateInformationTodoItem.IsComplete;
        }

        private async Task<TodoItem> GetTodoItemByIdOrThrow(long id) 
        {
            var todoItem = await _todoItemRepository.GetTodoItemByIdAsync(id)
                .ConfigureAwait(false);
            
            if (todoItem is null)
                throw new DataNotFoundException(id);

            return todoItem;
        }
    }
}
