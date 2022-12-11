using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.Domain.Todo;

namespace TodoApiDTO.Infrastructure.Services.Todo
{
    /// <summary>
    /// Получение данных из бд
    /// </summary>
    public interface ITodoItemsRepository
    {
        /// <summary>
        /// Получить список записей
        /// </summary>
        Task<IEnumerable<TodoItem>> GetItems();

        /// <summary>
        /// Получить конкретную запись
        /// </summary>
        /// <param name="itemId">Id записи</param>
        Task<TodoItem> GetItem(long itemId);


        /// <summary>
        /// Создать запись
        /// </summary>
        /// <returns>Id новой записи</returns>
        Task<long> CreateItem(string name, bool isComplete);


        /// <summary>
        /// Обновить запись
        /// </summary>
        Task UpdateItem(TodoItem item);


        /// <summary>
        /// Удалить запись
        /// </summary>
        Task DeleteItem(TodoItem item);
    }
}
