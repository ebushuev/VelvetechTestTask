using Application.Todo.Command.Create;
using Application.Todo.Command.Delete;
using Application.Todo.Command.Update;
using Application.Todo.Queries;
using Application.Todo.Queries.GetDetails;
using Application.Todo.Queries.GetList;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TodoApiDTO.Controllers;
using TodoApiDTO.Models;

namespace TodoApi.Controllers
{
    public class TodoItemsController : BaseController
    {

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<TodoDetailsListVm>> GetTodoItemsAsync()
        {
            return await Mediator.Send(new GetTodoListQuery());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDetailsVm>> GetTodoItemAsync(Guid id)
        {
            var result = await Mediator.Send(new GetTodoItemDetailsQuery { Id = id });
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateTodoItemAsync([FromBody] CreateTodoItemDTO dto)
        {
            var command = Mapper.Map<CreateTodoItemCommand>(dto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public async Task<IActionResult> UpdateTodoItemAsync([FromBody] UpdateTodoItemDTO dto)
        {
            var command = Mapper.Map<UpdateTodoItemCommand>(dto);
            await Mediator.Send(command);

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItemAsync(Guid id)
        {
            await Mediator.Send(new DeleteTodoItemCommand { Id = id});

            return NoContent();
        }
    }
}
