using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Domain.Interfaces;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoItemRepository _repository;

        public TodoService(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task AddTodoItemAsync(TodoItem item)
        {
            await _repository.AddAsync(item);
        }

        public async Task DeleteTodoItemAsync(long id)
        {
            await _repository.DeleteAsync(x => x.Id == id);
        }

        public async Task<List<TodoItem>> GetAllTodoItemsAsync()
        {
            var result = await _repository.GetAllAsync();

            return result;
        }

        public async Task<TodoItem> GetTodoItemByIdAsync(long id)
        {
            var result = await _repository.GetFirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<bool> IsTodoItemExistsAsync(long id)
        {
            return await _repository.AnyAsync(x => x.Id == id);
        }

        public async Task UpdateTodoItemAsync(TodoItem item)
        {
            await _repository.UpdateAsync(item);
        }
    }
}
