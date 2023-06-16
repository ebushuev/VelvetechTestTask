using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApiDTO.Services.Interfaces
{
    public interface ITodoService
    {
        public Task<IEnumerable<TodoItemDTO>> GetAll();
        public Task<TodoItemDTO> Get(long id);
        public Task<bool> Update(long id, TodoItemDTO todoItemDTO);
        public Task<TodoItemDTO> Create(TodoItemDTO todoItemDTO);
        public Task<bool> Delete(long id);
    }
}
