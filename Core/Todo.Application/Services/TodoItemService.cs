using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Application.DTO;
using Todo.Domain;
using Todo.Domain.Interfaces;

namespace Todo.Application.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task<TodoItemDTO> CreateItemAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            var newItem = await _todoItemRepository.CreateItemAsync(todoItem);

            return ItemToDTO(newItem);
        }

        public async Task DeleteItemAsync(long id)
        {
            await _todoItemRepository.DeleteItemAsync(id);
        }

        public async Task<TodoItemDTO> FindItemAsync(long id)
        {
            var todoItem = await _todoItemRepository.FindItemAsync(id);
            return todoItem != null ? ItemToDTO(todoItem) : null;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetItemsAsync()
        {
            var items = await _todoItemRepository.GetItemsAsync();
            return items.Select(x => ItemToDTO(x)).ToList();
        }

        public async Task UpdateTodoItemAsync(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                Id = id,
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete
            };

            await _todoItemRepository.UpdateTodoItemAsync(todoItem);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
