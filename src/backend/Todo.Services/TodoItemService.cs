using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.DataAccess.Repository.IRepository;
using Todo.Domain.Entities;

namespace Todo.Services
{
    internal class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            this.todoItemRepository = todoItemRepository;
        }

        public async Task<IEnumerable<TodoItem>> GetAsync()
        {
            return await todoItemRepository.GetAsync();
        }

        public async Task<TodoItem> GetAsync(long id)
        {
            return await todoItemRepository.GetAsync(id);
        }

        public async Task<long> CreateAsync(TodoItem item)
        {
            return await todoItemRepository.CreateAsync(item);

        }

        public async Task UpdateAsync(long id, TodoItem item)
        {
            var todoItem = await todoItemRepository.GetAsync(id);
            if (todoItem == null)
            {
                throw new KeyNotFoundException();
            }
            await todoItemRepository.UpdateAsync(id, todoItem);

        }

        public async Task DeleteAsync(long id)
        {
            var todoItem = await todoItemRepository.GetAsync(id);
            if (todoItem == null)
            {
                throw new KeyNotFoundException();
            }
            await todoItemRepository.DeleteAsync(todoItem.Id);
        }
    }
}