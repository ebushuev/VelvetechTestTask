using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Exceptions;
using Business.Services.Interfaces;
using DAL.Repositories.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class TodoItemsService : ITodoItemsService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoItemsService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        
        public async Task<IEnumerable<TodoItemDto>> GetTodoItems()
        {
            return (await _todoRepository.GetAllAsync()).Select(ItemToDto);
        }
        
        public async Task<TodoItemDto> GetTodoItem(long id)
        {
            TodoItem todoItem = await _todoRepository.GetByIdAsync(id);

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
            
            TodoItem todoItem = await _todoRepository.GetByIdAsync(id);
            
            if (todoItem == null)
            {
                throw new NotFoundException();
            }

            todoItem.Name = todoItemDto.Name;
            todoItem.IsComplete = todoItemDto.IsComplete;

            try
            {
                await _todoRepository.SaveChangesAsync();
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

            _todoRepository.Add(todoItem);
            await _todoRepository.SaveChangesAsync();

            return todoItem.Id;
        }
        
        public async Task DeleteTodoItem(long id)
        {
            TodoItem todoItem = await _todoRepository.GetByIdAsync(id);

            if (todoItem == null)
            {
                throw new NotFoundException();
            }

            _todoRepository.Remove(todoItem);
            await _todoRepository.SaveChangesAsync();
        }

        private bool TodoItemExists(long id)
        {
            return _todoRepository.AnyAsync(id).GetAwaiter().GetResult();
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