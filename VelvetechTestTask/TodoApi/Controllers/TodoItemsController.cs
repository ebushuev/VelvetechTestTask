using AutoMapper;
using BLL.Models;
using BLL.Services.Interfaces;
using FunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly IToDoService _toDoService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ToDoItemsController(IToDoService toDoService, IMapper mapper, ILogger logger)
        {
            _toDoService = toDoService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("Fetching all toDos from the storage");

            var toDos = await _toDoService.GetAllAsync()
                                          .FeedToAsync(_mapper.Map<IEnumerable<ToDoViewModel>>);

            _logger.LogInformation("All toDos fetched successful");

            return Ok(toDos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            _logger.LogInformation("Fetching the toDo from the storage");

            var toDo = await _toDoService.GetAsync(id)
                                         .FeedToAsync(_mapper.Map<ToDoViewModel>);

            _logger.LogInformation("The toDo fetched successful");

            return Ok(toDo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ToDoViewModel ToDoViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Adding the toDo to the storage");

            var toDo = await _mapper.Map<ToDoItem>(ToDoViewModel)
                                    .FeedTo(_toDoService.CreateAsync)
                                    .FeedToAsync(_mapper.Map<ToDoViewModel>);

            _logger.LogInformation("The toDo added successful");

            return Ok(toDo);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            _logger.LogInformation("Deleting the toDo from the storage");

            await _toDoService.DeleteByIdAsync(id);

            _logger.LogInformation("The toDo deleted successful");

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ToDoViewModel ToDoViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInformation("Updating the toDo");

            var toDo = await _mapper.Map<ToDoItem>(ToDoViewModel)
                                    .FeedTo(_toDoService.UpdateAsync)
                                    .FeedToAsync(_mapper.Map<ToDoViewModel>);

            _logger.LogInformation("The toDo updated successful");

            return Ok(toDo);
        }
    }
}
