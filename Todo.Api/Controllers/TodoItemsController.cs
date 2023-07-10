using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Todo.BLL.Services;
using Todo.Domain.DTOs;
using Todo.Domain.Exceptions;

namespace Todo.Api.Controllers
{
    [Route("api/todo-items")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoService _todoService;
        
        public TodoItemsController(TodoService todoService)
        {
            _todoService = todoService;
        }
        
        /// <summary>
        /// Get all todo items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var items = await _todoService.GetItems();
            return Ok(items);
        }
        
        /// <summary>
        /// Get todo item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoService.Get(id);
        
            if (todoItem == null)
            {
                throw new NotFoundException($"Todo item with id {id} not found");
            }
        
            return Ok(todoItem);
        }
        
        /// <summary>
        /// Update todo item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItemDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                throw new BadRequestException($"Invalid Todo item id {id}");
            }
            
            await _todoService.Update(todoItemDTO);
        
            return NoContent();
        }
        
        /// <summary>
        /// Create todo item
        /// </summary>
        /// <param name="todoItemDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = await _todoService.Create(todoItemDTO);
        
            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                todoItem);
        }
        
        /// <summary>
        /// Delete todo item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            await _todoService.Delete(id);
            return NoContent();
        }
    }
}
