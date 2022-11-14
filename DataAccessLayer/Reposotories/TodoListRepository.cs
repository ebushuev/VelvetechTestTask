using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using TodoApiDTO.BuisnessLayer.Models;
using TodoApiDTO.DataAccessLayer.Models;

namespace TodoApiDTO.DataAccessLayer.Reposotories
{
    /// <summary>
    /// Интефейс репозитория списка задач
    /// </summary>
    public interface ITodoListRepository
    {
        /// <summary>
        /// Получение всех задач
        /// </summary>
        /// <returns>Список всех задач</returns>
        Task<List<TodoItem>> GetTodoItems();
        /// <summary>
        /// Получение задачи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns>Задача</returns>
        Task<TodoItem> GetTodoItem(long id);
        /// <summary>
        /// Обновление задачи
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="todoItem">Задача</param>
        /// <returns>Обновленная задача</returns>
        Task<TodoItem> UpdateTodoItem(long id, TodoItem todoItem);
        /// <summary>
        /// Создание новой задачи
        /// </summary>
        /// <param name="todoItem">Задача</param>
        /// <returns>Новая задача</returns>
        Task<TodoItem> CreateTodoItem(TodoItem todoItem);
        /// <summary>
        /// Удаление задачи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        Task DeleteTodoItem(long id);
        /// <summary>
        /// Проверка на существование задачи
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns>true если существует, иначе false</returns>
        bool TodoItemExists(long id);
    }

    /// <summary>
    /// Репозиторий списка задач
    /// </summary>
    public class TodoListRepository : ITodoListRepository
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly TodoContext _context;

        /// <summary>
        /// Конструктор для инициализации БД
        /// </summary>
        /// <param name="context"></param>
        public TodoListRepository(TodoContext context)
        {
            _context = context;
        }


        public async Task<List<TodoItem>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }


        public async Task<TodoItem> GetTodoItem(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }



        public async Task<TodoItem> UpdateTodoItem(long id, TodoItem todoItem)
        {
            var existing = await _context.TodoItems.FindAsync(id);

            if (existing == null) return null;
            existing.Name = todoItem.Name;
            existing.IsComplete = todoItem.IsComplete;
            try
            {
                await _context.SaveChangesAsync();
                return existing;

            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return null;
            }

        }


        public async Task<TodoItem> CreateTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task DeleteTodoItem(long id)
        {
            var existing = await _context.TodoItems.FindAsync(id);

            if (existing != null)
            {
                _context.TodoItems.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }

        public bool TodoItemExists(long id) =>
           _context.TodoItems.Any(e => e.Id == id);


    }
}
