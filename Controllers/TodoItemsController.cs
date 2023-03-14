using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ITodoService todoService, ILogger<TodoItemsController> logger)
        {
            _todoService = todoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            try
            {
                return await _todoService.GetList()
                    .Select(x => TodoItemDTO.ItemToDTO(x))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetTodoItems: {ex.Message}\n{ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            if (id < 0)
            {
                _logger.LogError($"GetTodoItem: invalid id value ({id})");
                return BadRequest("Invalid id");
            }

            try
            {
                var todoItem = await _todoService.GetById(id);
                if (todoItem == null)
                {
                    _logger.LogError($"GetTodoItem: item not found (id:{id})");
                    return NotFound("TODO item not found");
                }

                return TodoItemDTO.ItemToDTO(todoItem);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetTodoItem (id:{id}): {ex.Message}\n{ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id < 0)
            {
                _logger.LogError($"UpdateTodoItem: invalid id value ({id})");
                return BadRequest("Invalid id");
            }
            if (todoItemDTO == null)
            {
                _logger.LogError($"UpdateTodoItem: request is null (id:{id})");
                return BadRequest("Request is null");
            }

            if (id != todoItemDTO.Id)
            {
                _logger.LogError($"UpdateTodoItem: ids mismatch (parameter id:{id}, request id:{todoItemDTO?.Id})");
                return BadRequest("Ids mismatch");
            }

            try
            {
                var result = await _todoService.Update(todoItemDTO);
                return HandleActionResult(result, $"UpdateTodoItem (id:{id})");
            }
            catch (Exception ex)
            {
                _logger.LogError($"UpdateTodoItem (id:{id}): {ex.Message}\n{ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }

        [Route("create")]
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            try
            {
                var todoItem = await _todoService.Create(todoItemDTO);
                if (todoItem == null)
                {
                    _logger.LogError($"CreateTodoItem: cannot create item");
                    return BadRequest("Couldn't create TODO item");
                }

                return CreatedAtAction(
                    nameof(GetTodoItem),
                    new { id = todoItem.Id },
                    TodoItemDTO.ItemToDTO(todoItem));
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateTodoItem: {ex.Message}\n{ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            if (id < 0)
            {
                _logger.LogError($"DeleteTodoItem: invalid id ({id})");
                return BadRequest("Invalid id");
            }

            try 
            {
                var result = await _todoService.Delete(id);
                return HandleActionResult(result, $"DeleteTodoItem (id:{id})");
            }
            catch (Exception ex)
            {
                _logger.LogError($"DeleteTodoItem (id:{id}): {ex.Message}\n{ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }
        
        private IActionResult HandleActionResult(TodoItemActionResult result, string actionName)
        {
            switch (result)
            {
                case TodoItemActionResult.Success:
                    return NoContent();
                case TodoItemActionResult.NotFound:
                    _logger.LogError($"{actionName}: item not found");
                    return NotFound("TODO item not found");
                default:
                    return NoContent();
            }
        }
    }
}
