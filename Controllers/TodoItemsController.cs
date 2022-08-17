using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todo.Domain.Interfaces;
using TodoApiDTO.Models;
using System.Linq;
using AutoMapper;
using Todo.Domain.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;
        public TodoItemsController(ITodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = _todoRepository.GetAll();
            var result = items.Select(m => _mapper.Map<TodoItemDTO>(m)).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(long id)
        {
            var todoItem = await _todoRepository.Get(id);

            if (todoItem != null)
            {
                var result = _mapper.Map<TodoItemDTO>(todoItem);

                return Ok(result);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoItemDTO todoItemDTO)
        {
            var item = new TodoItemModel(null, todoItemDTO.Name);

            var result = await _todoRepository.Create(item);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _todoRepository.Delete(id);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Complete(long id)
        {
            var todoItem = await _todoRepository.Get(id);

            if (todoItem != null)
            {
                var model = new TodoItemModel(todoItem.Id, todoItem.Name, todoItem.IsComplete);
                model.Complete();
                await _todoRepository.Update(model);

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UnComplete(long id)
        {
            var todoItem = await _todoRepository.Get(id);

            if (todoItem != null)
            {
                var model = new TodoItemModel(todoItem.Id, todoItem.Name, todoItem.IsComplete);
                model.UnComplete();
                await _todoRepository.Update(model);

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeName(long id, string name)
        {
            var todoItem = await _todoRepository.Get(id);

            if (todoItem != null)
            {
                var model = new TodoItemModel(todoItem.Id, todoItem.Name, todoItem.IsComplete);
                model.UpdateName(name);
                await _todoRepository.Update(model);

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
