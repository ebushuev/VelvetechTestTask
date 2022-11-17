using System.Threading.Tasks;
using TodoApiDTO.DAL.Entities;

namespace TodoApiDTO.DAL.Repositories.Abstractions
{
    public interface ITodoItemRepository : IBaseRepository<TodoItem>
    {
        bool TodoItemExists(long id);
    }
}
