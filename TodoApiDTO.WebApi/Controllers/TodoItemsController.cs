using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.Application.Todo;
using TodoApiDTO.WebApi.Models.Todo;
using Microsoft.Extensions.Logging;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetItems()
        {
            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(GetItems)} Start.");

            var items = await _todoItemsService.GetItems();
            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(GetItems)} Found items: {items.Count()}");
            _logger.LogDebug($"{nameof(TodoItemsController)}.{nameof(GetItems)} End.");
            return Ok(items);
        }

        [HttpGet("{id}")]
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

        [HttpPost]
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

        [HttpPut("{id}")]
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

        [HttpPost("{id}/complete")]
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

        [HttpDelete]
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
