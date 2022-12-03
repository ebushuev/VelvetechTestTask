namespace TodoApiDTO.Components.TodoList.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using TodoApiDTO.Components.TodoList.Dto;
    using TodoApiDTO.Components.TodoList.Interfaces;
    using TodoApiDTO.Components.TodoList.Models;
    using TodoApiDTO.Exceptions;

    /// <summary>
    /// Реализация специфичных для сущности
    /// TO-DO CRUD операций.
    /// </summary>
    /// <remarks>
    /// Данный сервис слежит для того, чтобы
    /// убрать из контроллера логику с обработкой
    /// исключений, выборкой данных и использовать
    /// только готовые специфичные CRUD операции.
    /// </remarks>
    public class TodoCrudService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoCrudService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoItemDto>> GetTodoItems()
        {
            var allItems = await _todoRepository
                .GetAll();

            return allItems
                .Select(TodoItemDto.Projection)
                .ToArray();
        }

        public async Task<TodoItemDto> GetTodoItem(long id)
        {
            var todoItem = await _todoRepository.Get(id);

            if (todoItem == null)
            {
                throw new ValidationException($"Запись с Id {id} не найдена в БД.");
            }

            return TodoItemDto.Projection(todoItem);
        }

        public async Task UpdateTodoItem(long id, TodoItemDto todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                throw new ValidationException($"Идентификаторы сущности для обновления ({todoItemDto.Id}) и "
                    + $"обнолвяемой сущности ({id}) не совпадают");
            }

            var todoItem = await _todoRepository.Get(id);

            if (todoItem == null)
            {
                throw new ValidationException($"Запись с Id {id} не найдена в БД.");
            }

            todoItem.Name = todoItemDto.Name;
            todoItem.IsComplete = todoItemDto.IsComplete;

            await _todoRepository.Update(todoItem);
        }

        public async Task<TodoItemDto> CreateTodoItem(TodoItemDto todoItemDto)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDto.IsComplete,
                Name = todoItemDto.Name
            };

            await _todoRepository.Create(todoItem);

            return TodoItemDto.Projection(todoItem);
        }

        public async Task DeleteTodoItem(long id)
        {
            var todoItem = await _todoRepository.Get(id);

            if (todoItem == null)
            {
                throw new ValidationException($"Запись с Id {id} не найдена в БД.");
            }

            await _todoRepository.Delete(todoItem.Id);
        }
    }
}