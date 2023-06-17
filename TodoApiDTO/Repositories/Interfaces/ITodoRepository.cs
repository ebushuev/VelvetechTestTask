using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.DTOs;

namespace TodoApiDTO.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        public Task<IEnumerable<TodoItemDTO>> GetAll();
        public Task<TodoItemDTO> Get(long id);
        public Task<bool> Update(long id, CreateUpdateItemTodoDTO todoItem);
        public Task<TodoItemDTO> Create(CreateUpdateItemTodoDTO todoItem);
        public Task<bool> Delete(long id);
    }
}
