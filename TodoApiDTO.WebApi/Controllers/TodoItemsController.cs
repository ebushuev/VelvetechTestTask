using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.Application.Todo;
using TodoApiDTO.WebApi.Models.Todo;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace TodoApiDTO.WebApi.Controllers
{
    public class TodoItemsController : ApiControllerBase
    {
        private readonly ITodoItemsService _todoItemsService;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(
            ITodoItemsService todoItemsService,
            ILogger<TodoItemsController> logger) 
        {
            _todoItemsService = todoItemsService;
            _logger = logger;
        }

        /// <summary>
        /// Получить список задач
        /// </summary>
        /// <returns>Список дел</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Получить список задач")]
        [SwaggerResponse(StatusCodes.Status200OK, "Список задач", typeof(IEnumerable<TodoItemDto>))]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetItems()
        {
            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(GetItems)} Start.");

            var items = await _todoItemsService.GetItems();
            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(GetItems)} Found items: {items.Count()}");
            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(GetItems)} End.");
            return Ok(items);
        }

        /// <summary>
        /// Получить определенную задачу
        /// </summary>
        /// <param name="id">Id задачи</param>
        /// <response code="200">Информация по определенной задачи</response>
        /// <response code="400">Ошибка в Id задачи</response>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Получить определенную задачу")]
        [SwaggerResponse(StatusCodes.Status200OK, "Информация по задаче", typeof(TodoItemDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в Id задачи")]
        public async Task<ActionResult<TodoItemDto>> GetItem(long id)
        {
            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(GetItem)} Start.");
            if (id < 1)
            {
                return BadRequest("incorrect id");
            }

            var item = await _todoItemsService.GetItem(id);

            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(GetItem)} End.");
            return Ok(item);
        }

        /// <summary>
        /// Создать задачу
        /// </summary>
        /// <param name="item">Информация по задаче</param>
        /// <response code="200">Задача создалась</response>
        /// <response code="400">Пришли пустые данные</response>
        [HttpPost]
        [SwaggerOperation(Summary = "Создать задачу")]
        [SwaggerResponse(StatusCodes.Status200OK, "Задача успешно создалась")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Пришли пустые данные")]
        public async Task<ActionResult> CreateItem(CreateTodoItem item)
        {
            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(CreateItem)} Start.");

            if (item == null)
            {
                return BadRequest("data is empty");
            }

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                return BadRequest("incorrect name");
            }

            await _todoItemsService.Create(item.Name, item.IsComplete);

            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(CreateItem)} End.");
            return Ok();
        }

        // Можно и так оставить, но думаю стоит разделить этот функционала
        //[HttpPut("{id}")]
        //public async Task<ActionResult> UpdateItem(long id, UpdateTodoItem item)
        //{
        //    _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(UpdateItem)} Start.");
        //    if ((id < 1) || (id != item.Id))
        //    {
        //        return BadRequest("incorrect id");
        //    }

        //    if (string.IsNullOrWhiteSpace(item.Name) && !item.IsComplete.HasValue)
        //    {
        //        return BadRequest("incorrect data");
        //    }

        //    await _todoItemsService.UpdateItem(id, item.Name, item.IsComplete);

        //    _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(UpdateItem)} End.");
        //    return Ok();
        //}

        /// <summary>
        /// Обновить текст задачи
        /// </summary>
        /// <param name="id">Id задачи</param>
        /// <param name="item">Информация о задаче</param>
        /// <response code="200">Задача обновилась</response>
        /// <response code="400">Пришли некорректные данные</response>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Обновить текст задачи")]
        [SwaggerResponse(StatusCodes.Status200OK, "Задача обновилась")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Пришли некорректные данные")]
        public async Task<ActionResult> UpdateItem(long id, UpdateTodoItem item)
        {
            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(UpdateItem)} Start.");
            if ((id < 1) || (id != item.Id))
            {
                return BadRequest("incorrect id");
            }

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                return BadRequest("incorrect data");
            }

            await _todoItemsService.UpdateItem(id, item.Name);

            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(UpdateItem)} End.");
            return Ok();
        }

        /// <summary>
        /// Завершить задачу
        /// </summary>
        /// <param name="id">id задачи</param>
        /// <response code="200">Задача отмеченна как завершенна</response>
        /// <response code="400">Ошибка в Id задачи</response>
        [HttpPost("{id}/complete")]
        [SwaggerOperation(Summary = "Завершить задачу")]
        [SwaggerResponse(StatusCodes.Status200OK, "Задача отмеченна как завершенна")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в Id задачи")]
        public async Task<ActionResult> CompleteItem(long id)
        {
            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(CompleteItem)} Start.");
            if (id < 1)
            {
                return BadRequest("incorrect id");
            }

            await _todoItemsService.CompleteItem(id);

            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(CompleteItem)} End.");
            return Ok();
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="id">id задачи</param>
        /// <response code="200">Задача удалена</response>
        /// <response code="400">Ошибка в Id задачи</response>
        [HttpDelete]
        [SwaggerOperation(Summary = "Удалить задачу")]
        [SwaggerResponse(StatusCodes.Status200OK, "Задача удалена")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Ошибка в Id задачи")]
        public async Task<ActionResult> DeleteItem(long id)
        {
            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(DeleteItem)} Start.");

            if (id < 1)
            {
                return BadRequest("incorrect id");
            }

            await _todoItemsService.DeleteItem(id);

            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(DeleteItem)} End.");
            return Ok();
        }
    }
}
