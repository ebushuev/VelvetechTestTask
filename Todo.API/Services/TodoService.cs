using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.DAL.Models;
using Todo.DAL.Repositories;


namespace Todo.API.Services
{
    /// <inheritdoc/>
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        /// <summary></summary>
        public TodoService(ITodoRepository todoRepository) =>
            _todoRepository = todoRepository;

        /// <inheritdoc/>
        public async Task<TodoItem> GetAsync(long id) =>
            await _todoRepository.GetAsync(id);

        /// <inheritdoc/>
        public Task<List<TodoItem>> GetAsync() =>
            _todoRepository.GetAsync();

        /// <inheritdoc/>
        public async Task AddAsync(TodoItem todoItem)
        {
            _todoRepository.Add(todoItem);
            await _todoRepository.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(TodoItem todoItem)
        {
            var item = await _todoRepository.GetAsync(todoItem.Id);
            if (item == null)
                throw new ArgumentOutOfRangeException("Не найдена задача с указанным Id");

            item.Name = todoItem.Name;
            item.IsComplete = todoItem.IsComplete;
            try
            {
                await _todoRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exception) when (!TodoItemExists(todoItem.Id))
            {
                throw new ArgumentOutOfRangeException("Ошибка параллелизма! Не найдена задача с указанным Id", exception);
            }
        }

        /// <inheritdoc/>
        public async Task RemoveAsync(long id)
        {
            var todoItem = await _todoRepository.GetAsync(id);
            if (todoItem == null)
                throw new ArgumentOutOfRangeException("Не найдена задача с указанным Id");

            _todoRepository.Remove(todoItem);
            await _todoRepository.SaveChangesAsync();
        }

        private bool TodoItemExists(long id) =>
            _todoRepository.Get().Any(e => e.Id == id);
    }
}
