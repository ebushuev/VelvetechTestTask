using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Components.TodoList.Models;

namespace TodoApiDTO.Components.TodoList.Interfaces
{
    /// <summary>
    /// Интерфейс 'Репозиторий' для сущности TO-DO.
    /// </summary>
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAll();

        Task<TodoItem> Get(long id);

        Task Create(TodoItem item);

        Task Update(TodoItem item);

        Task Delete(long id);

        Task<bool> Exists(long id);
    }
}