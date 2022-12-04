namespace TodoApiDTO.Components.TodoList.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TodoApiDTO.Components.TodoList.Models;

    /// <summary>
    /// Интерфейс 'Репозиторий' для сущности TO-DO.
    /// </summary>
    public interface ITodoRepository
    {
        /// <summary>
        /// Получение всего набора записей.
        /// </summary>
        Task<IEnumerable<TodoItem>> GetAll();

        /// <summary>
        /// Получение записи по идентификатору.
        /// </summary>
        Task<TodoItem> Get(long id);

        /// <summary>
        /// Создание новой записи.
        /// </summary>
        Task Create(TodoItem item);

        /// <summary>
        /// Обновление существующей записи.
        /// </summary>
        Task Update(TodoItem item);

        /// <summary>
        /// Удаление существующей записи.
        /// </summary>
        Task Delete(long id);

        Task<bool> Exists(long id);
    }
}