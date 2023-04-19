using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.BL.Interfaces;
using TodoApiDTO.Models;

namespace TodoApiDTO.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController( ITodoItemService context )
        {
            _todoItemService = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var todoItems = await _todoItemService.GetTodoItemsAsync();
            return Ok( todoItems );
        }

        [HttpGet( "{id}" )]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem( long id )
        {
            var todoItem = await _todoItemService.GetTodoItemAsync( id );

            if( todoItem == null ) {
                return NotFound();
            }

            return Ok( todoItem );
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> UpdateTodoItem( long id, TodoItemDTO todoItemDTO )
        {
            if( id != todoItemDTO.Id ) {
                return BadRequest();
            }

            try {
                await _todoItemService.UpdateTodoItemAsync( id, todoItemDTO );
            }
            catch( Exception ) {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem( TodoItemDTO todoItemDTO )
        {
            if( !ModelState.IsValid ) {
                return BadRequest( ModelState );
            }

            var createdTodoItem = await _todoItemService.CreateTodoItemAsync( todoItemDTO );
            return CreatedAtAction( nameof( GetTodoItem ), new
            {
                id = createdTodoItem.Id
            }, createdTodoItem );
        }

        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteTodoItem( long id )
        {
            try {
                await _todoItemService.DeleteTodoItemAsync( id );
            }
            catch( Exception ) {
                return NotFound();
            }

            return NoContent();
        }
    }
}
