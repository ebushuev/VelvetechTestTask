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
    [ExceptionLoggingFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoItemsController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemResponse>>> GetTodoItems()
        {
            var items = await _todoService.GetItems();
            return items.Select(x => x.ToResponse()).ToList();
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemRequest request)
        {
            if (id != request.Id) return BadRequest();

            await _todoService.UpdateItem(id, request.ToDto());
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemResponse>> CreateTodoItem(TodoItemRequest item)
        {
            var result = await _todoService.CreateItem(item.ToDto());
            return CreatedAtAction(nameof(result), result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _todoService.DeleteItem(id);
            return Ok();
        }
    }
}
