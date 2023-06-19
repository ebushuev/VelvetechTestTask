using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.DAL.Repositories
{
    public interface ITodoItemRepository
    {
        public Task<List<TodoItem>> GetAll();
        public Task<TodoItem> GetById(long todoItemId);
        public Task Create(TodoItem newItem);

        public void Delete(TodoItem item);
        Task SaveAsync();
    }
}
