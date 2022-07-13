using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.TodoApiDTO.Infrastructure.DataLayer;
using TodoApiDTO.ToDoApiModels.Models;
using TodoApiDTO.ToDoApiModels.ModelsDTO;
using System;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace TodoApiDTO.Service
{
    public class ToDoService : DbContext
    {
        public ToDoService(DbContextOptions<TodoContext> options) 
           : base(options)
        {
            Database.EnsureCreated();
        }

        public System.Data.Entity.DbSet<TodoItem> TodoItems { get; set; }

        private readonly TodoContext _context;

        public ToDoService(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItemDTO>> GetTodoItems()
        {
           var result = await _context.TodoItems
            .Select(x => ItemToDTO(x))
            .ToListAsync();

            return result;
        }

        public async Task<TodoItem> GetTodoItem(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<TodoItem> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var item = await _context.TodoItems.FindAsync(id);

            item.Name = todoItemDTO.Name;
            item.IsComplete = todoItemDTO.IsComplete;

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<TodoItem> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        public async Task<TodoItem> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
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
