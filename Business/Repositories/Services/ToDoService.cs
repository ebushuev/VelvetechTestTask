using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Business.DTO;
using TodoApi.DataAccess.Models;
using TodoApiDTO.Business.Repositories.Interface;

namespace TodoApiDTO.Business.Repositories.Services
{
    public class ToDoService: IToDoService
    {
        private readonly TodoContext _context;
        private readonly ILoggerService _logger;

        public ToDoService(TodoContext context, ILoggerService logger)
        {
            _logger = logger;
            _context = context;
        }
        /// <summary>
        /// Create new ToDo Item
        /// </summary>
        /// <param name="todoItemDTO"></param>
        /// <returns></returns>
        public async Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return ItemToDTO(todoItem);
        }
        /// <summary>
        /// Delete ToDo Item by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return false;
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Get ToDo Item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TodoItemDTO> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            return ItemToDTO(todoItem);
        }
        /// <summary>
        /// Get List of ToDo Items
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TodoItemDTO>> GetTodoItems() => await _context.TodoItems.Select(x => ItemToDTO(x)).ToListAsync();
        /// <summary>
        /// Update ToDo Item
        /// </summary>
        /// <returns></returns>
        public async Task<TodoItemDTO> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return null;
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex) when (!TodoItemExists(id))
            {
                _logger.LogError(ex.Message);
                return null;
            }
            return ItemToDTO(todoItem);
        }
        private bool TodoItemExists(long id) =>
             _context.TodoItems.Any(e => e.Id == id);

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
