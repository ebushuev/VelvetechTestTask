using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Models;
using TodoApiDTO.Repository;
using TodoApiDTO.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private ToDoItemService _service;

        public TodoItemsController(ToDoItemService service)
        {
            _service = service;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "��������� ���� �������",
            Description = "����� �������������� ��������� ���� ������� �� ���� ������",
            OperationId = "GetTodoItems"
        )]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var items = await _service.GetTodoItems();
            if (items is null)
            {
                return NoContent();
            }
            return Ok();
        }

        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "��������� ������� �� ��������� ID",
            Description = "����� �������������� ��������� ������� �� ��������� ID �� ���� ������",
            OperationId = "GetTodoItem"
        )]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var item = await _service.GetTodoItem(id);
            return Ok();
        }

        [HttpPut("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "���������� ������� �� ��������� ID",
            Description = "����� �������������� ���������� ������� �� ��������� ID �� ���� ������",
            OperationId = "UpdateTodoItem"
        )]
        public async Task<IActionResult> UpdateTodoItem(UpdateTodoItemRequest model)
        {
                await _service.UpdateTodoItem(model);
                return Ok();
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "�������� �������",
            Description = "����� �������������� �������� �������",
            OperationId = "UpdateTodoItem"
        )]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(CreateTodoItemRequest model)
        {
            return Ok(await _service.CreateTodoItem(model));
        }

        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "�������� ������� �� ��������� ID",
            Description = "����� �������������� �������� ������� �� ��������� ID",
            OperationId = "UpdateTodoItem"
        )]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
                await _service.DeleteTodoItem(id);
                return Ok();
        }   
    }
}
