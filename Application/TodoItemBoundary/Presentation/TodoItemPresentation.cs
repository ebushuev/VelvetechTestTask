using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Application.TodoItemBoundary.Extensions;
using TodoApi.Application.TodoItemBoundary.Models;
using TodoApi.Infrastructure.Data.Contexts;
using TodoApi.Infrastructure.Data.Models;
using TodoApiDTO.Application.Exceptions;

namespace TodoApiDTO.Application.TodoItemBoundary.Presentation
{
    public class TodoItemPresentation : ITodoItemPresentation
    {
        private readonly TodoContext _context;

        public TodoItemPresentation(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAllAsync()
        {
            return await _context.TodoItems.Select(x => x.ItemToDTO()).ToListAsync();

            //return todoItems.Select(x => x.ItemToDTO()).ToList();
        }

        public async Task<TodoItem> GetTodoItemByIdAsync(long id)
            => await _context.TodoItems.FindAsync(id);

        public bool TodoItemExists(long id)
           => _context.TodoItems.Any(e => e.Id == id);

        public async Task<TodoItemDTO> GetTodoItemDTOByIdAsync(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            return todoItem?.ItemToDTO();
        }

        public async Task<TodoItem> CreateTodoItemAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            _ = await _context.SaveChangesAsync();

            return todoItem;
        }

        public async Task DeleteByEntityAsync(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task Update(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await GetTodoItemByIdAsync(id) ?? throw new NotFoundException();

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            await _context.SaveChangesAsync();
        }
    }
}
