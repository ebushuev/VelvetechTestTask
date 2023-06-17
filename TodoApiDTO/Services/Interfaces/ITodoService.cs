using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.DTOs;

namespace TodoApiDTO.Services.Interfaces
{
    public interface ITodoService
    {
        public Task<IEnumerable<TodoItemDTO>> GetAll();
        public Task<TodoItemDTO> Get(long id);
        public Task<bool> Update(long id, CreateUpdateItemTodoDTO createUpdateItemTodo);
        public Task<TodoItemDTO> Create(CreateUpdateItemTodoDTO createUpdateItemTodo);
        public Task<bool> Delete(long id);
    }
}
