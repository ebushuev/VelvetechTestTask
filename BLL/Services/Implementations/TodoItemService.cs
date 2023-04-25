using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApiDTO.BLL.Services;
using TodoApiDTO.DAL.Entities;
using TodoApiDTO.DAL.Repositories;


namespace TodoApiDTO.BLL.Implementations.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository repository;
        
        public TodoItemService(ITodoItemRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync()
        {
            var todoItems = await repository.GetTodoItemsAsync();
            return todoItems
                .Select(x => ItemToDTO(x));
        }

        public async Task<TodoItemDTO> GetTodoItemAsync(long id)
        {
            var todoItem = await repository.GetTodoItemByIdAsync(id);
            var todoItemDTO = todoItem != null ? ItemToDTO(todoItem) : null;
            return todoItemDTO;
        }
        public async Task<TodoItemDTO> AddAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            await repository.InsertTodoItemAsync(todoItem);
            await repository.SaveAsync();
            return ItemToDTO(todoItem);
        }
        public async Task EditAsync(TodoItemDTO todoItemDTO)
        {
           await repository.UpdateTodoItemAsync(todoItemDTO);

            try
            {
                await repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(todoItemDTO.Id))
            {
                throw new TodoItemException("Item not found");
            }
        }
        public async Task DeleteAsync(long id)
        {
            await repository.DeleteTodoItemAsync(id);
            await repository.SaveAsync();
            
            
        }
        private bool TodoItemExists(long id) => 
            repository.GetTodoItemsAsync().Result.Any(e => e.Id == id);

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
