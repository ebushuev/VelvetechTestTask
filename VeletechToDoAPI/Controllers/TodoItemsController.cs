using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoCore.Data.Entities;
using TodoCore.DTOs;
using TodoCore.Exceptions;
using TodoCore.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IAddTodoItemService _addTodoItemService;
        private readonly IGetTodoItemService _getTodoItemService;
        private readonly IGetTodoItemsService _getTodoItemsService;
        private readonly IUpdateTodoItemService _updateTodoItemService;
        private readonly IDeleteTodoItemService _deleteTodoItemService;

        public TodoItemsController(IAddTodoItemService addTodoItemService, IGetTodoItemService getTodoItemService, IGetTodoItemsService getTodoItemsService, 
            IUpdateTodoItemService updateTodoItemService, IDeleteTodoItemService deleteTodoItemService)
        {
            _addTodoItemService = addTodoItemService;
            _getTodoItemService = getTodoItemService;
            _getTodoItemsService = getTodoItemsService;
            _updateTodoItemService = updateTodoItemService;
            _deleteTodoItemService = deleteTodoItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var result = await _getTodoItemsService.GetTodoItemsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            try
            {
                var result = await _getTodoItemService.GetTodoItemAsync(id);
                return Ok(result);
            }
            catch(EntityNotFoundException<TodoItem> ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            try
            {
                var result = await _updateTodoItemService.UpdateTodoItemAsync(todoItemDTO);
                return Ok(result);
            }
            catch(EntityNotFoundException<TodoItem> ex)
            {
                return NotFound(ex.Message);
            }
            catch(SomethingWentWrongException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            await _addTodoItemService.AddTodoItemAsync(todoItemDTO);
            return Ok(todoItemDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            try
            {
                var result = await _deleteTodoItemService.DeleteTodoItemAsync(id);
                return Ok();
            }
            catch (EntityNotFoundException<TodoItem>)
            {
                return NotFound();
            }
            catch (SomethingWentWrongException)
            {
                return StatusCode(500);
            }
        }
    }
}
