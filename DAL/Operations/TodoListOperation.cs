using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using TodoApi.Models;
using TodoApi.Data;
using Microsoft.CodeAnalysis;

namespace TodoApi.Operations
{
    public class TodoListOperation : ITodoListOperation
    {
        private readonly TodoContext _context;
        public TodoListOperation(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemAsync(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void AddTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
        }

        public void RemoveTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
        }

        public bool TodoItemExists(long id) =>
             _context.TodoItems.Any(e => e.Id == id);

    }
}
