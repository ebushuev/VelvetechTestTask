using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.DAL.DataContexts;
using TodoApiDTO.DAL.Entities;
using TodoApi.Models;

namespace TodoApiDTO.DAL.Repositories.Implementations
{
    
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext context;

        public TodoItemRepository(TodoContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<TodoItem>> GetTodoItemsAsync()
        {
            return await context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> GetTodoItemByIdAsync(long id)
        {
            
           return await context.TodoItems.FindAsync(id); 
        }

        public async Task InsertTodoItemAsync(TodoItem todoItem)
        {
            await context.TodoItems.AddAsync(todoItem);
        }

        public async Task DeleteTodoItemAsync(long todoItemID)
        {
            TodoItem todoItem = await context.TodoItems.FindAsync(todoItemID);
            if (todoItem != null)
            {
                context.TodoItems.Remove(todoItem);
            }
        }

        public async Task UpdateTodoItemAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = await GetTodoItemByIdAsync(todoItemDTO.Id);
            if (todoItem != null)
            {
                todoItem.Name = todoItemDTO.Name;
                todoItem.IsComplete = todoItemDTO.IsComplete;
            }
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
        
    }
}
