using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using TodoApiDTO.BuisnessLayer.Models;
using TodoApiDTO.DataAccessLayer.Models;
using TodoApiDTO.DataAccessLayer.Reposotories;

namespace TodoApiDTO.BuisnessLayer.Services
{
    /// <summary>
    /// Интефейс сервиса списка задач
    /// </summary>
    public interface ITodoListService
    {
        /// <summary>
        /// Получение всех задач
        /// </summary>
        /// <returns>Список всех задач</returns>
        Task<List<TodoItemDTO>> GetTodoItems();
        /// <summary>
        /// Получение задачи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns>Задача</returns>
        Task<TodoItemDTO> GetTodoItem(long id);
        /// <summary>
        /// Обновление задачи
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <param name="todoItemDTO">Задача</param>
        /// <returns>Обновленная задача</returns>
        Task<TodoItemDTO> UpdateTodoItem(long id, TodoItemDTO todoItemDTO);
        /// <summary>
        /// Создание новой задачи
        /// </summary>
        /// <param name="todoItemDTO">Задача</param>
        /// <returns>Новая задача</returns>
        Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO);
        /// <summary>
        /// Удаление задачи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        Task DeleteTodoItem(long id);

    }

    /// <summary>
    /// Сервис списка задач
    /// </summary>
    public class TodoListService : ITodoListService
    {
        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Репозиторий списка задач
        /// </summary>
        private readonly ITodoListRepository _todoListRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="todoListRepository">Репозиторий списка задач</param>
        public TodoListService(IMapper mapper, ITodoListRepository todoListRepository)
        {
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }


        public async  Task<List<TodoItemDTO>> GetTodoItems()
        {
            var result = await _todoListRepository.GetTodoItems();
            var resultDto = _mapper.Map<List<TodoItemDTO>>(result);
            return resultDto;
        }

      

        public async Task<TodoItemDTO> GetTodoItem(long id)
        {
            var todoItem = await _todoListRepository.GetTodoItem(id);
            var resultDto = _mapper.Map<TodoItemDTO>(todoItem);

            return resultDto;
        }

        public async Task<TodoItemDTO> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = _mapper.Map<TodoItem>(todoItemDTO);

            var result = await _todoListRepository.UpdateTodoItem(id, todoItem);

            var resultDTO = _mapper.Map<TodoItemDTO>(result);

            return resultDTO;

        }

        public async Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = _mapper.Map<TodoItem>(todoItemDTO);
            var result = await _todoListRepository.CreateTodoItem(todoItem);
            var resultDTO = _mapper.Map<TodoItemDTO>(result);
            return resultDTO;

        }

        public async Task DeleteTodoItem(long id)
        {
            await _todoListRepository.DeleteTodoItem(id);
        }
    }
}
