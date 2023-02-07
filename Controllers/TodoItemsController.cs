using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using TodoApi.Application.TodoItems.Contract;
using TodoApi.DataLayer.DataAccess;
using TodoApi.DataLayer.Dto;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TodoItemsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var result = await _mediator.Send(new GetTodoItemsRequest());

            return result
                .Select(_mapper.Map<TodoItemDTO>)
                .ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _mediator.Send(new GetTodoItemRequest
            {
                Id = id
            });

            if (todoItem == null)
            {
                return NotFound();
            }

            return _mapper.Map<TodoItemDTO>(todoItem);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDto)
        {
            // TODO: Exception handling should be done via middleware
            try
            {
                await _mediator.Send(new UpdateTodoItemRequest
                {
                    Id = id,
                    TodoItem = _mapper.Map<TodoItem>(todoItemDto)
                });
            }
            catch (ValidationException)
            {
                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDto)
        {
            var todoItem = await _mediator.Send(new CreateTodoItemRequest
            {
                TodoItem = _mapper.Map<TodoItem>(todoItemDto)
            });

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                _mapper.Map<TodoItemDTO>(todoItem));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            try
            {
                await _mediator.Send(new DeleteTodoItemRequest
                {
                    Id = id,
                });
            }
            catch (ValidationException)
            {
                return BadRequest();
            }
            catch (EntityNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}