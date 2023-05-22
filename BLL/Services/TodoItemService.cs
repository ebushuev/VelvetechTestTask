using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApiDTO.BLL.DTO;
using TodoApiDTO.BLL.Interfaces;
using TodoApiDTO.DAL.Models;

namespace TodoApiDTO.BLL.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly TodoContext _context;

        public TodoItemService(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItemDto>> GetTodoItems()
        {
            return await _context.TodoItems
                .Select(x => ItemToDto(x))
                .ToListAsync();
        }

        public async Task<TodoItemDto> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return null;
            }

            return ItemToDto(todoItem);
        }

        public async Task<bool> UpdateTodoItem(long id, TodoItemDto todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                return false;
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return false;
            }

            todoItem.Name = todoItemDto.Name;
            todoItem.IsComplete = todoItemDto.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return false;
                }
                throw;
            }

            return true;
        }

        public async Task<TodoItemDto> CreateTodoItem(TodoItemDto todoItemDto)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDto.IsComplete,
                Name = todoItemDto.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return ItemToDto(todoItem);
        }

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

        private bool TodoItemExists(long id) =>
            _context.TodoItems.Any(e => e.Id == id);

        private static TodoItemDto ItemToDto(TodoItem todoItem) =>
            new TodoItemDto
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}