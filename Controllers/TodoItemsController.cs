using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.BusinessLayer;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IBusinessLayer _businessLayer;

        public TodoItemsController(IBusinessLayer businessLayer)
        {
            _businessLayer = businessLayer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return (await _businessLayer.ExecuteTodoItemFetch()).Payload;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            OperationEventResult result = await _businessLayer.ExecuteTodoItemFetch(id);

            return result.Type switch
            {
                OperationEventType.NotFound => NotFound(),
                OperationEventType.Done => result.Payload.First(),
                _ => throw new DbUpdateException("Unhandled exception occured.")
            };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            OperationEventResult result = await _businessLayer.ExecuteTodoItemUpdate(id, todoItemDTO);

            return result.Type switch
            {
                OperationEventType.BadRequest => BadRequest(),
                OperationEventType.NotFound => NotFound(),
                OperationEventType.NoContent => NoContent(),
                _ => throw new DbUpdateException("Unhandled exception occured.")
            };
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            OperationEventResult result = await _businessLayer.ExecuteTodoItemCreate(todoItemDTO);
            TodoItemDTO resultDTO = result.Payload.First();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = resultDTO.Id },
                resultDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            OperationEventResult result = await _businessLayer.ExecuteTodoItemDelete(id);

            return result.Type switch
            {
                OperationEventType.NotFound => NotFound(),
                OperationEventType.NoContent => NoContent(),
                _ => throw new DbUpdateException("Unhandled exception occured.")
            };
        }      
    }
}
