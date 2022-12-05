using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ToDo.Application.Models;
using ToDo.Application.Services;
using ToDo.DAL.Exceptions;
using ToDo.WebApi.Models;

namespace ToDo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _service;
        private readonly IMapper _mapper;

        public TodoItemsController(ITodoItemService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets ToDoItem list
        /// </summary>
        /// <returns>List of ToDoItems</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemVM>>> GetTodoItems()
        {
            var items = await _service.GetAllAsync();
            var result =  _mapper.Map<IEnumerable<TodoItemVM>>(items);
            return Ok(result);
        }

        /// <summary>
        /// Gets ToDoItem by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ToDoItem</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemVM>> GetTodoItem(int id)
        {
            try
            {
                var item = await _service.GetAsync(id);
                var result = _mapper.Map<TodoItemVM>(item);
                return Ok(result);
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e); // log
                return NotFound();
            }
        }

        /// <summary>
        /// Updates existing ToDoItem
        /// </summary>
        /// <param name="id">item to update</param>
        /// <param name="todoItemVm">attributes to update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(int id, TodoItemVM todoItemVm)
        {
            if (id != todoItemVm.Id)
            {
                return BadRequest();
            }

            try
            {
                //await _service.GetAsync(id);
                await _service.UpdateAsync(_mapper.Map<ToDoDto>(todoItemVm));
                
                return Ok();
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e); // log
                return NotFound();
            }

        }

        /// <summary>
        /// Creates new item
        /// </summary>
        /// <param name="todoItemVm">new item</param>
        /// <returns>Added item</returns>
        [HttpPost]
        public async Task<ActionResult<TodoItemVM>> CreateTodoItem(TodoItemVM todoItemVm)
        {
            try
            {
                var item = await _service.CreateAsync(_mapper.Map<ToDoDto>(todoItemVm));
                var result = _mapper.Map<TodoItemVM>(item);
                return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, result);
            }
            catch (AlreadyExistsException e)
            {
                Console.WriteLine(e);
                return NoContent();
            }
        }

        /// <summary>
        /// Deletes item by id
        /// </summary>
        /// <param name="id">item id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            try
            {   
                await _service.DeleteAsync(id);
            }
            catch (NotFoundException e)
            {
                Console.WriteLine(e);
                return NotFound();
            }

            return NoContent();
        }
    }
}
