using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using Domain;
using DataAccessLayer.DTOs;
using Application.IServices;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private ITodoListRepository _todoListRepository;
        //private TodoContext _todoContext;


        public TodoItemsController(ITodoListRepository todoListRepository, TodoContext todoContext)
        {
            _todoListRepository = todoListRepository;
            //_todoContext = todoContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var todoItems = await _todoListRepository.GetTodoItem();

            return todoItems.ToList();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoListRepository.GetTodoItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _todoListRepository.GetTodoItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            var updated = await _todoListRepository.UpdateTodoItem(todoItemDTO);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var createdTodoItem = await _todoListRepository.CreateTodoItem(todoItemDTO);

            if (createdTodoItem == null)
            {
                BadRequest();
            }

            return createdTodoItem;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _todoListRepository.GetTodoIt(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            var deletedTodoItem = await _todoListRepository.DeleteTodoItem(todoItem);

            if (!deletedTodoItem)
            {
                BadRequest();
            }

            return NoContent();
        }
    }
}
