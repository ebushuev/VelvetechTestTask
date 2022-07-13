using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using TodoApi.Application.TodoItemBoundary.Extensions;
using TodoApi.Application.TodoItemBoundary.Models;
using TodoApiDTO.Application.Exceptions;
using TodoApiDTO.Application.TodoItemBoundary.Presentation;

namespace TodoApi.Controllers
{
    [Route("api/[controller][action]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemPresentation _todoItemPresentation;

        public TodoItemsController(ITodoItemPresentation todoItemPresentation)
        {
            _todoItemPresentation = todoItemPresentation;
        }

        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TodoItemDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return Ok(
                await _todoItemPresentation.GetAllAsync()
                );
        }

        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoItemPresentation.GetTodoItemDTOByIdAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                await _todoItemPresentation.Update(id, todoItemDTO);
            }
            catch (DbUpdateConcurrencyException) when (!_todoItemPresentation.TodoItemExists(id))
            {
                return NotFound();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = await _todoItemPresentation.CreateTodoItemAsync(todoItemDTO);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                todoItem.ItemToDTO()
                );
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _todoItemPresentation.GetTodoItemByIdAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            await _todoItemPresentation.DeleteByEntityAsync(todoItem);
            return NoContent();
        }
    }
}
