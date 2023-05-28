using Application.Features.ToDo.CreateTodoItem;
using Application.Features.ToDo.DeleteTodoItem;
using Application.Features.ToDo.GetToDoItems;
using Application.Features.ToDo.GetToDoItemsById;
using Application.Features.ToDo.UpdateTodoItem;
using Domain;
using Infrastructure.DbContexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private IMediator _mediator;

        public TodoItemsController(IMediator mediator)
        {       
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<TodoItem>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ICollection<TodoItem>>> GetTodoItems()
        {
            var result = await _mediator.Send(new GetTodoItemsQuery());

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TodoItemDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var result = await _mediator.Send(new GetTodoItemsByIdQuery()
            {
                TodoItemId = id
            });

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _mediator.Send(new GetTodoItemsByIdQuery()
            {
                TodoItemId = id
            });

            if (todoItem == null)
            {
                return NotFound();
            }

            await _mediator.Send(new UpdateTodoItemQuery()
            {
                Id = id,
                TodoItem = new TodoItem
                {
                    Name = todoItemDTO.Name,
                    IsComplete = todoItemDTO.IsComplete
                }
            });

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(typeof(TodoItemDTO), 201)]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = await _mediator.Send(new CreateTodoItemQuery()
            {
                TodoItem = new TodoItem
                {
                    Name = todoItemDTO.Name,
                    IsComplete = todoItemDTO.IsComplete
                }
            });            

            return CreatedAtAction(
            nameof(GetTodoItem),
            new { id = todoItem.Id },
            ItemToDTO(todoItem));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var result = await _mediator.Send(new GetTodoItemsByIdQuery()
            {
                TodoItemId = id
            });

            if (result == null)
            {
                return NotFound();
            }

            await _mediator.Send(new DeleteTodoItemQuery
            {
                TodoItemId = id
            });

            return NoContent();

        }
      
        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };       
    }
}
