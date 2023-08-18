
namespace TodoApi.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetTodoItemAsync(long id);
        Task<IEnumerable<T>> GetTodoItemsAsync();
        Task UpdateTodoItemAsync(T todoItemDTO);
        Task<T> CreateTodoItemAsync(T todoItemDTO);
        Task DeleteTodoItemAsync(long id);
    }
}
