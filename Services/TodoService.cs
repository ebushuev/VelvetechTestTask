using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Models;

namespace TodoApiDTO.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoItem> Create(TodoItemDTO createInput)
        {
            if (createInput == null) throw new ArgumentNullException(nameof(createInput));
            return await _todoRepository.Create(createInput);
        }

        public async Task<TodoItemActionResult> Delete(long itemToDeleteId)
        {
            return await _todoRepository.Delete(itemToDeleteId);
        }

        public async Task<TodoItem> GetById(long itemId)
        {
            return await _todoRepository.GetById(itemId);
        }

        public async Task<TodoItemActionResult> Update(TodoItemDTO updateInput)
        {
            if (updateInput == null) throw new ArgumentNullException(nameof(updateInput));
            return await _todoRepository.Update(updateInput);
        }

        public DbSet<TodoItem> GetList()
        {
            return _todoRepository.GetList();
        }
    }
}
