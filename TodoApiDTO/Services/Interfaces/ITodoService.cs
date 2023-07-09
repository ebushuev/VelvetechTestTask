using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.ApiConstans;
using TodoApiDTO.DTOs;

namespace TodoApiDTO.Services.Interfaces
{
    public interface ITodoService
    {
        public Task<IEnumerable<TodoItemDTO>> GetAll();
        public Task<TodoItemDTO> Get(long id);
        public Task<ApiResponseStatus> Update(long id, CreateUpdateItemTodoDTO createUpdateItemTodo);
        public Task<TodoItemDTO> Create(CreateUpdateItemTodoDTO createUpdateItemTodo);
        public Task<ApiResponseStatus> Delete(long id);
    }
}
