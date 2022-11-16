using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Exceptions;
using Business.Services.Interfaces;
using DAL;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class TodoItemsService : ITodoItemsService
    {
        private readonly TodoContext _context;

        public TodoItemsService(TodoContext context)
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
            TodoItem todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                throw new NotFoundException();
            }
            
            return ItemToDto(todoItem);
        }
                
        public async Task UpdateTodoItem(long id, TodoItemDto todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                throw new Exception("Id was not equals to model's id");
            }
            
            TodoItem todoItem = await _context.TodoItems.FindAsync(id);
            
            if (todoItem == null)
            {
                throw new NotFoundException();
            }

            todoItem.Name = todoItemDto.Name;
            todoItem.IsComplete = todoItemDto.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                throw new NotFoundException();
            }
        }
        
        public async Task<long> CreateTodoItem(TodoItemDto todoItemDto)
        {
            TodoItem todoItem = new TodoItem
            {
                IsComplete = todoItemDto.IsComplete,
                Name = todoItemDto.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return todoItem.Id;
        }
        
        public async Task DeleteTodoItem(long id)
        {
            TodoItem todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                throw new NotFoundException();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDto ItemToDto(TodoItem todoItem)
        {
            return new TodoItemDto
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
        }
    }
}