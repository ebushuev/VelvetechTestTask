using BusinessLayer.DTOs;
using BusinessLayer.Services.Interface;
using DataLayer.Context;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController(ITodoItemService toodoItemService, ILogger<TodoItemsController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _todoItemService = toodoItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var todoItems = await _todoItemService.GetTodoItems();
            try
            {  
                if (todoItems == null)
                {
                    _logger.LogInformation($"Empty Table");
                    return NotFound();
                }
                else
                {
                    return new List<TodoItemDTO>(todoItems);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exeption error ", ex);
                return StatusCode(500, "Server has problem");
            }

           ;
        }

        [HttpGet("{id}",Name = "GetTodoItem")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {

            var todoItem = await _todoItemService.GetTodoItem(id);


            try
            {
                if (todoItem == null)
                {
                    _logger.LogInformation($"Can not find ItemId={id}");
                    return NotFound();
                }
                else
                {
                    return todoItem;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exeption error ", ex);
                return StatusCode(500, "Server has problem");
            }


            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, CreateOrUpdateTodoItemDTO createOrUpdateTodoItemDTO)
        {

            if (!ModelState.IsValid)
            {
                //todo add loging

                return BadRequest();
            }

            var todoItem = await _todoItemService.GetTodoItem(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = createOrUpdateTodoItemDTO.Name;
            todoItem.IsComplete = createOrUpdateTodoItemDTO.IsComplete;

            try
            {
                 _todoItemService.UpdateTodoItem(id, createOrUpdateTodoItemDTO);
            }
            catch (DbUpdateConcurrencyException) when (!_todoItemService.TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(CreateOrUpdateTodoItemDTO todoItemDTO)
        {
            if (!ModelState.IsValid)
            {
                //todo add loging
                return BadRequest();
            }

            var todoItem = new CreateOrUpdateTodoItemDTO()
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

         var id=   _todoItemService.CreateTodoItem(todoItem);
       

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = id.Id },
                ItemToDTO(todoItem,id.Id));
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {

            if (!ModelState.IsValid)
            {
                //todo add loging
                return BadRequest();
            }

            var todoItem = await _todoItemService.GetTodoItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }
            _todoItemService.DeleteTodoItem(id);
            return NoContent();
        }



        private static TodoItemDTO ItemToDTO(CreateOrUpdateTodoItemDTO todoItem,long id) =>
            new TodoItemDTO
            {
                Id = id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
