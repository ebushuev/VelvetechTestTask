using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.BL.Interfaces;

namespace TodoApi.Controllers
{
    [Route ( "api/[controller]" )]
    [ApiController]
    public class TodoItemsController : ControllerBase {
        private readonly IService<TodoItemDTO> service;

        public TodoItemsController( IService<TodoItemDTO> service ) {
            this.service = service ?? throw new ArgumentNullException ( nameof ( service ) );
        }

        /// <summary>
        /// Get all TodoItems
        /// </summary>
        /// <returns>Return <see cref="IEnumerable{TodoItemDTO}"/> collection.</returns>
        [HttpGet]
        public IEnumerable<TodoItemDTO> GetTodoItems() {
            return service.GetAll ();
        }

        /// <summary>
        /// Get an TodoItem by its id
        /// </summary>
        /// <param name="id">Unique id of TodoItem</param>
        /// <returns>Found TodoItem or null</returns>
        [HttpGet ( "{id}" )]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem( long id ) {
            return await service.Get ( id );
        }

        /// <summary>
        /// Update properties of TodoItem
        /// </summary>
        /// <param name="id">Unique id of TodoItem</param>
        /// <param name="todoItemDTO">TodoItemDTO with new properties</param>
        /// <returns>Result's status code of operation</returns>
        [HttpPut ( "{id}" )]
        public async Task<IActionResult> UpdateTodoItem( long id, TodoItemDTO todoItemDTO ) {
            return StatusCode ( await service.Update ( id, todoItemDTO ) );
        }

        /// <summary>
        /// Creates new TodoItem and add it to repository
        /// </summary>
        /// <param name="todoItemDTO">TodoItemDTO which uses for creating new TodoItem</param>
        /// <returns>Result's status code of operation</returns>
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem( TodoItemDTO todoItemDTO ) {
            return StatusCode ( await service.Create ( todoItemDTO ) );
        }

        /// <summary>
        /// Removes todoItem with matching id
        /// </summary>
        /// <param name="id">Unique id of TodoItem</param>
        /// <returns>Result's status code of operation</returns>
        [HttpDelete ( "{id}" )]
        public async Task<IActionResult> DeleteTodoItem( long id ) {
            return StatusCode ( await service.Delete ( id ) );
        }
    }
}
