using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Application.Exceptions;
using Todo.Domain;
using Todo.Domain.Interfaces;
using Todo.Persistence.Models;

namespace Todo.Persistence.Services
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext _context;

        public TodoItemRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> FindItemAsync(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task UpdateTodoItemAsync(TodoItem todoItem)
        {
            var todoItemDb = await FindItemAsync(todoItem.Id);

            if (todoItemDb == null)
            {
                throw new NotFoundException("todoItem not found!");
            }

            todoItemDb.Name = todoItem.Name;
            todoItemDb.IsComplete = todoItem.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(todoItem.Id))
            {
                throw new NotFoundException();
            }
        }

        public async Task DeleteItemAsync(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                throw new NotFoundException();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem> CreateItemAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);

            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool TodoItemExists(long id) =>
             _context.TodoItems.Any(e => e.Id == id);
    }
}
