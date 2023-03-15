using System.Threading.Tasks;

namespace TodoApiDTO.Domain
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> GetById(long id);
        Task<long> Create(TodoItem item);
        Task Save(TodoItem item);
        Task Delete(TodoItem item);
    }
}
