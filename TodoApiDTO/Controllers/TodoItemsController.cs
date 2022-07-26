using Application.Common;
using Application.Items;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using TodoApiDTO.Controllers;

namespace TodoApi.Controllers
{
    public class TodoItemsController: BaseApiController
    {

        [HttpGet]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Returns all items",
            typeof(PagedList<TodoItemDTO>))]
        public async Task<IActionResult> GetTodoItems([FromQuery] PagingParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [HttpGet("{id}")]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Successfully returns it",
            typeof(TodoItemDTO))]
        public async Task<IActionResult> GetTodoItem(long id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPut("{id}")]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Item is updated successfully",
            typeof(Unit))]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            todoItemDTO.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { TodoItem = todoItemDTO }));
        }

        [HttpPost]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Creates item successfully",
           typeof(Unit))]
        public async Task<IActionResult> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            return HandleResult(await Mediator.Send(new Create.Command { TodoItem = todoItemDTO }));
        }

        [HttpDelete("{id}")]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Item is deleted successfully",
           typeof(Unit))]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}
