using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Interfaces;
using TodoApiDTO.Mappers;
using TodoApiDTO.Repositories;

namespace TodoApiDTO.Services
{
    public class TodoItemService : ITodoItemService
    {
        private IRepository<TodoItem> _todoItemRepository;

        public TodoItemService(IRepository<TodoItem> todoItemRepository) 
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task<List<TodoItemDTO>> GetTodoItems()
        {
            return await _todoItemRepository.List().Select(x => x.Map()).ToListAsync();
        }

        public async Task<TodoItemDTO> GetTodoItem(long id)
        {
            var todoItem = await _todoItemRepository.FindAsync(id);
            return todoItem.Map();
        }

        public async Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var newItem = await  _todoItemRepository.Create(todoItemDTO.Map());
            return newItem.Map();
        }

        public async Task<bool> DeleteTodoItem(long id)
        {
            return await _todoItemRepository.Remove(id);
        }

        public async Task<bool> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await _todoItemRepository.FindAsync(id);
            if (todoItem == null)
            {
                return false;
            }
            try
            {
                return await _todoItemRepository.Update(todoItemDTO.Map());
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return false;
            }
        }

        private bool TodoItemExists(long id) =>
             _todoItemRepository.List().Any(e => e.Id == id);
    }
}
