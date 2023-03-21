using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApiDTO.Core.Models;
using TodoApiDTO.Core.Services;
using TodoApiDTO.EF.Entities;

namespace TodoApiDTO.EF.Services
{
    public class TodoService : ITodoService
    {
        #region Static

        private static TodoItemDTO EntityToDTO(TodoItem todoItem)
        {
            return new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
        }

        private static TodoItem DTOToEntity(TodoItemDTO todoItem)
        {
            return new TodoItem
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
        }

        #endregion

        private readonly TodoContext _context;

        public TodoService(TodoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private async Task<TodoItem> GetEntityRequiredAsync(long id)
        {
            var entity = await _context.TodoItems.FindAsync(id);

            if (entity == null)
            {
                throw new InvalidOperationException($"Entity id={id} not exist.");
            }

            return entity;
        }

        #region ITodoService Members

        public Task<List<TodoItemDTO>> GetAllAsync()
        {
            return _context.TodoItems
                .Select(x => EntityToDTO(x))
                .ToListAsync();
        }

        public async Task<TodoItemDTO> FindAsync(long id)
        {
            var entity = await _context.TodoItems.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            return EntityToDTO(entity);
        }

        public Task<bool> GetIsExistAsync(long id)
        {
            return _context.TodoItems.AnyAsync(x => x.Id == id);
        }

        public async Task<TodoItemDTO> CreateAsync(TodoItemCreateDTO dto)
        {
            var entity = new TodoItem
            {
                IsComplete = dto.IsComplete,
                Name = dto.Name
            };

            _context.TodoItems.Add(entity);
            await _context.SaveChangesAsync();

            return EntityToDTO(entity);
        }

        public async Task UpdateAsync(TodoItemDTO dto)
        {
            var entity = await GetEntityRequiredAsync(dto.Id);

            var next = DTOToEntity(dto);
            _context.Entry(entity).CurrentValues.SetValues(next);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await GetEntityRequiredAsync(id);

            _context.TodoItems.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<bool> GetNameIsUsedAsync(string name)
        {
            return _context.TodoItems.AnyAsync(x => x.Name == name);
        }

        public Task<bool> GetNameIsUsedExceptOneAsync(long id, string name)
        {
            return _context.TodoItems.AnyAsync(x => x.Name == name && x.Id != id);
        }

        #endregion
    }
}