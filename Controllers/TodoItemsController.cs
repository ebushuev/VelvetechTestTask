using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Interfaces;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly ITodoItemService _itemService;

        public TodoItemsController(ILogger<TodoItemsController> logger, ITodoItemService itemService)
        {
            _logger = logger;
            _itemService = itemService;
        }

        /// <summary>
        /// List all todo items
        /// </summary>
        /// <returns>List of existing todo items</returns>      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return await _itemService.GetTodoItems();
        }

        /// <summary>
        /// Return concrete todo item
        /// </summary>
        /// <param name="id">Id of desired todo item</param>
        /// <returns>Concrete todo item</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _itemService.GetTodoItem(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return todoItem;
        }

        /// <summary>
        /// Update todo item's attributes
        /// </summary>
        /// <param name="id">Id of updating todo item</param>
        /// <param name="todoItemDTO">New model of todo item</param>
        /// <returns>Request status</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }
            var result = await _itemService.UpdateTodoItem(id, todoItemDTO);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        /// <summary>
        /// Create new todo item
        /// </summary>
        /// <param name="todoItemDTO">New todo item's model</param>
        /// <returns>New todo item's model</returns>
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var newItem = await _itemService.CreateTodoItem(todoItemDTO);
            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = newItem.Id },
                newItem);
        }

        /// <summary>
        /// Remove concrete todo item
        /// </summary>
        /// <param name="id">Id of todo item to delete</param>
        /// <returns>Request status</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var result = await _itemService.DeleteTodoItem(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
