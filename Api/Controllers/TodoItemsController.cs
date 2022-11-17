using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Dtos;
using Business.Exceptions;
using Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsService _todoItemsService;
        private readonly ILogger<TodoItemsController> _logger;

        public TodoItemsController(ITodoItemsService todoItemsService, ILogger<TodoItemsController> logger)
        {
            _todoItemsService = todoItemsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
        {
            try
            {
                return Ok(await _todoItemsService.GetTodoItems());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error has occurred during todos retrieving");
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
        {
            try
            {
                TodoItemDto todoItemDto = await _todoItemsService.GetTodoItem(id);

                return Ok(todoItemDto);
            }
            catch (NotFoundException e)
            {
                _logger.LogInformation(e, "An item was not found");
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error has occurred during todo retrieving");
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDto todoItemDto)
        {
            try
            {
                await _todoItemsService.UpdateTodoItem(id, todoItemDto);

                return NoContent();
            }
            catch (NotFoundException e)
            {
                _logger.LogInformation(e, "An item to update was not found");
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error has occurred during todo updating");
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> CreateTodoItem(TodoItemDto todoItemDto)
        {
            try
            {
                long id = await _todoItemsService.CreateTodoItem(todoItemDto);
                
                todoItemDto.Id = id;
                
                return CreatedAtAction(nameof(GetTodoItem), new { id }, todoItemDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error has occurred during todo creating");
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            try
            {
                await _todoItemsService.DeleteTodoItem(id);

                return NoContent();
            }
            catch (NotFoundException e)
            {
                _logger.LogInformation(e, "An item to delete was not found");
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error has occurred during todo deleting");
                return BadRequest(e.Message);
            }
        }     
    }
}
