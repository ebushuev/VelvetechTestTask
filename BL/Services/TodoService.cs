using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.BL.DTO;
using TodoApi.DAL;
using TodoApi.DAL.Models;

namespace TodoApiDTO.BL.Services
{
    /// <summary>
    /// Интерфейс сервиса управления задачами
    /// </summary>
    public interface ITodoService
    {
        /// <summary>
        /// Получение списка задач
        /// </summary>
        /// <returns>Список задач</returns>
        Task<IEnumerable<TodoItemDTO>> GetTodoItems();

        /// <summary>
        /// Получение задачи
        /// </summary>
        /// <param name="id">id задачи</param>
        /// <returns>Задача</returns>
        Task<TodoItemDTO> GetTodoItem(long id);

        /// <summary>
        /// Обновление задачи
        /// </summary>
        /// <param name="id">id задачи</param>
        /// <param name="todoItemDTO">Задача</param>
        /// <returns>Задача</returns>
        Task<TodoItemDTO> UpdateTodoItem(long id, TodoItemDTO todoItemDTO);

        /// <summary>
        /// Создание задачи
        /// </summary>
        /// <param name="todoItemDTO">Задача</param>
        /// <returns>Задача</returns>
        Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO);

        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="id">id задачи</param>
        /// <returns>Удалена ли задача</returns>
        Task<bool> DeleteTodoItem(long id);
    }

    /// <summary>
    /// Сервис управления задачами
    /// </summary>
    public class TodoService : ITodoService
    {
        private readonly TodoContext _context;
        private readonly ILogger<TodoService> _logger;

        public TodoService(TodoContext context, ILogger<TodoService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Получение списка задач
        /// </summary>
        /// <returns>Список задач</returns>
        public async Task<IEnumerable<TodoItemDTO>> GetTodoItems()
        {
            return await _context.TodoItems
                 .Select(x => ItemToDTO(x))
                 .ToListAsync();
        }

        /// <summary>
        /// Получение задачи
        /// </summary>
        /// <param name="id">id задачи</param>
        /// <returns>Задача</returns>
        public async Task<TodoItemDTO> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                _logger.LogError("Нет задачи с таким id");
                return null;
            }

            return ItemToDTO(todoItem);
        }

        /// <summary>
        /// Обновление задачи
        /// </summary>
        /// <param name="id">id задачи</param>
        /// <param name="todoItemDTO">Задача</param>
        /// <returns></returns>
        public async Task<TodoItemDTO> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                _logger.LogError("Аргумент id не совпадает с id задачи");
                return null;
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return null;
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
                return ItemToDTO(todoItem);
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                _logger.LogError("DbUpdateConcurrencyException");
                return null;
            }
        }

        /// <summary>
        /// Создание задачи
        /// </summary>
        /// <param name="todoItemDTO">Задача</param>
        /// <returns>Задача</returns>
        public async Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return ItemToDTO(todoItem);
        }

        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="id">id задачи</param>
        /// <returns>Удалена ли задача</returns>
        public async Task<bool> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return false;
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return true;
        }

        private bool TodoItemExists(long id) =>
             _context.TodoItems.Any(e => e.Id == id);

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
