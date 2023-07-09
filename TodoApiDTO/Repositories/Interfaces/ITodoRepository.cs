using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.ApiConstans;

namespace TodoApiDTO.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        public Task<IEnumerable<TodoItem>> GetAll();
        public Task<TodoItem> Get(long id);
        public Task<ApiResponseStatus> Update(long id, TodoItem todoItem);
        public Task<TodoItem> Create(TodoItem todoItem);
        public Task<ApiResponseStatus> Delete(long id);
    }
}
