using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.BLL.Dto;
using TodoApi.BLL.Interfaces;
using TodoApi.DAL.Models;

namespace TodoApi.BLL.Implementation
{
    public class TodoItemsService : ITodoItemsService
    {
        private readonly IRepository<TodoItem> _repository;

        public TodoItemsService(IRepository<TodoItem> repository)
        {
            _repository = repository;
        }


        public async Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(i => ItemToDTO(i));
        }

        public async Task<bool> UpdateTodoItemAsync(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                throw new Exception("id != todoItemDTO.Id");
            }

            var todoItem = await _repository.GetByIdAsync(id);
            if (todoItem == null)
            {
                return false;
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                await _repository.UpdateAsync(todoItem);
            }
            catch (DbUpdateConcurrencyException) when (_repository.GetById(id) == null)
            {
                return false;
            }

            return true;
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

        public async Task<TodoItemDTO> GetTodoItemAsync(long id)
        {
            var todoItem = await _repository.GetByIdAsync(id);

            if (todoItem == null)
            {
                return null;
            }

            return ItemToDTO(todoItem);
        }

        public async Task<TodoItemDTO> CreateTodoItemAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            await _repository.AddAsync(todoItem);

            return ItemToDTO(todoItem);
        }

        public async Task<bool> DeleteTodoItemAsync(long id)
        {
            var todoItem = await _repository.GetByIdAsync(id);

            if (todoItem == null)
            {
                return false;
            }

            await _repository.DeleteAsync(todoItem);

            return true;
        }
    }
}
