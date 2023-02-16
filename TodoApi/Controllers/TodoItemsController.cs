using Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.Infrastructure;
using TodoApiDTO.Models;

namespace TodoApiDTO.Controllers
{
    [NRExceptionsFilter]
    [ExceptionLoggingFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoItemsController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Gets the todo items.
        /// </summary>
        /// <returns>Todo items.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemResponse>>> GetTodoItems()
        {
            var items = await _todoService.GetItems();
            return items.Select(x => x.ToResponse()).ToList();
        }

        /// <summary>
        /// Gets the todo item by specified id.
        /// </summary>
        /// <param name="id">The element id.</param>
        /// <returns>The todo item.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemResponse>> GetTodoItem(long id)
        {
            var todoItem = await _todoService.GetItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem.ToResponse();
        }

        /// <summary>
        /// Updates the todo item.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="request">Updated data.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemRequest request)
        {
            await _todoService.UpdateItem(id, request.ToDto());
            return Ok();
        }

        /// <summary>
        /// Creates new item.
        /// </summary>
        /// <param name="item">Item to create.</param>
        /// <returns>Created item.</returns>
        [HttpPost]
        public async Task<ActionResult<TodoItemResponse>> CreateTodoItem(TodoItemRequest item)
        {
            var result = await _todoService.CreateItem(item.ToDto());
            return new ObjectResult(result) { StatusCode = 200 };
        }

        /// <summary>
        /// Deletes the item by specified Id.
        /// </summary>
        /// <param name="id">The item id.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _todoService.DeleteItem(id);
            return Ok();
        }
    }
}
