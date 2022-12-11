using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TodoApiDTO.Application.Todo
{
    /// <summary>
    /// Сервис работы с записями TodoItem
    /// </summary>
    public interface ITodoItemsService
    {
        /// <summary>
        /// Получить список всех записей
        /// </summary>
        Task<IEnumerable<TodoItemDto>> GetItems();

        /// <summary>
        /// Получить конкретную запись
        /// </summary>
        /// <param name="id">Id записи</param>
        Task<TodoItemDto> GetItem(long id);

        /// <summary>
        /// Создать запись
        /// </summary>
        Task Create(string name, bool isComplete);

        /// <summary>
        /// обновить запись
        /// </summary>
        Task UpdateItem(long id, string name, bool? isComplete);

        /// <summary>
        /// Удалить запись
        /// </summary>
        Task DeleteItem(long id);
    }
}
