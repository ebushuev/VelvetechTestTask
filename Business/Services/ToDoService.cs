using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using TodoApi.Models;
using TodoApiDTO.Business.DTO;
using TodoApiDTO.DataAccess;

namespace TodoApiDTO.Business.Services
{
    public class ToDoService
    {
        private readonly TodoContext _context;

        public ToDoService(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItems()
        {
            return await _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        public async Task<TodoItemDTO> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            return ItemToDTO(todoItem);
        }

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
                return ItemToDTO(todoItem);

            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return null;
            }
        }

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

        public async Task DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem != null)
            {
                _context.TodoItems.Remove(todoItem);
                await _context.SaveChangesAsync();
            }
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
