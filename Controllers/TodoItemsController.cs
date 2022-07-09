using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.DAL.Models;
using TodoApi.Database;
using TodoApi.Models;
using TodoApiDTO.ToDoApi.DAL.Repositories.Implementation;
using ToDoItems.DAL.Interfaces;

namespace TodoApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
 
        /// <summary>
        /// logger
        /// </summary>
        private readonly ILogger<TodoItemsController> _logger;

        /// <summary>
        /// tasks repository
        /// </summary>
        private IRepository<TodoItem> _todoItemsRepository;

        public TodoItemsController(ILogger<TodoItemsController> logger, ToDoContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            _todoItemsRepository = new ToDoItemsRepository(dbContext);

            if (logger == null)
                throw new ArgumentNullException(nameof(logger));
            _logger = logger;  
        }

        /// <summary>
        /// get all tasks
        /// </summary>
        /// <returns>list od all tasks</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            try
            {
                var items = await _todoItemsRepository.GetAllAsync();
                _logger.LogDebug($"{nameof(GetTodoItems)}: {items.Count()} items found");
                return items.Select(x => new TodoItemDTO(x)).ToList();
            }

            catch(Exception e)
            {
                _logger.LogError(($"{nameof(GetTodoItems)}: {e.Message}"));
                return StatusCode(500);
            }
           
        }
        /// <summary>
        /// get todo item by id
        /// </summary>
        /// <param name="id">item id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            try
            {
                var todoItem = await _todoItemsRepository.FindAsync(id);

                if (todoItem == null)
                {
                    _logger.LogError($"{nameof(GetTodoItem)}: Item {id} not found");
                    return NotFound();
                }
                return new TodoItemDTO(todoItem);
            }
            catch (Exception e)
            {
                _logger.LogError(($"{nameof(GetTodoItem)}: {e.Message}"));
                return StatusCode(500);
            }
        }

        /// <summary>
        /// update existing item
        /// </summary>
        /// <param name="id">item id</param>
        /// <param name="todoItemDTO">new item data</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            try
            {

                if (id != todoItemDTO.Id)
                {
                    _logger.LogError($"{nameof(UpdateTodoItem)}: item id {id} and item data {todoItemDTO.Id} don`t match");
                    return BadRequest();
                }

                var todoItem = await _todoItemsRepository.FindAsync(id);
                if (todoItem == null)
                {
                    _logger.LogError($"{nameof(UpdateTodoItem)}: Item {id} not found");
                    return NotFound();
                }

                todoItem.Name = todoItemDTO.Name;
                todoItem.IsComplete = todoItemDTO.IsComplete;

                await _todoItemsRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e) when (!_todoItemsRepository.Any(e => e.Id == id))
            {
                _logger.LogError(($"{nameof(UpdateTodoItem)}: {e.Message}"));
                return NotFound();
            }

            catch (Exception e)
            {
                _logger.LogError(($"{nameof(UpdateTodoItem)}: {e.Message}"));
                return StatusCode(500);
            }

            return NoContent();
        }

        /// <summary>
        /// Add new to do item
        /// </summary>
        /// <param name="todoItemDTO">new item data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            try
            {
                var todoItem = new TodoItem
                {
                    IsComplete = todoItemDTO.IsComplete,
                    Name = todoItemDTO.Name
                };

                _todoItemsRepository.Add(todoItem);
                await _todoItemsRepository.SaveChangesAsync();

                _logger.LogDebug(($"{nameof(CreateTodoItem)}: item {todoItem.Id},  {todoItem.Name} created"));

                return CreatedAtAction(
                    nameof(GetTodoItem),
                    new { id = todoItem.Id },
                    new TodoItemDTO(todoItem));
            }
            catch (Exception e)
            {
                _logger.LogError(($"{nameof(CreateTodoItem)}: {e.Message}"));
                return StatusCode(500);
            }
        }

        /// <summary>
        /// delete item by id
        /// </summary>
        /// <param name="id">item id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            try
            {
                var todoItem = await _todoItemsRepository.FindAsync(id);

                if (todoItem == null)
                {
                    _logger.LogError($"{nameof(DeleteTodoItem)}: Item {id} not found");
                    return NotFound();
                }

                _todoItemsRepository.Delete(todoItem);
                await _todoItemsRepository.SaveChangesAsync();
                _logger.LogDebug(($"{nameof(DeleteTodoItem)}: item {id} deleted"));
            }

            catch (Exception e)
            {
                _logger.LogError(($"{nameof(DeleteTodoItem)}: {e.Message}"));
                return StatusCode(500);
            }

            return NoContent();
        }
    
    }
}
