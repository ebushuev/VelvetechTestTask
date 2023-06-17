using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Repositories.Interfaces;
using TodoApiDTO.Services.Interfaces;

namespace TodoApiDTO.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;
        private readonly IMapper _mapper;


        public TodoService(ITodoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAll()
        {
            var todos = await _repository.GetAll();
            var dtos = todos.Select(todo => _mapper.Map<TodoItemDTO>(todo));
            return dtos;
        }

        public async Task<TodoItemDTO> Get(long id)
        {
            var todo = await _repository.Get(id);
            return _mapper.Map<TodoItemDTO>(todo);
        }

        public async Task<bool> Update(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await _repository.Get(id);
            if (todoItem == null)
                return false;
            todoItem = _mapper.Map<TodoItem>(todoItemDTO);
            return await _repository.Update(id, todoItem);
        }

        public async Task<TodoItemDTO> Create(TodoItemDTO todoItemDTO)
        {
            var todoItem = _mapper.Map<TodoItem>(todoItemDTO);

            var todo = await _repository.Create(todoItem);
            return _mapper.Map<TodoItemDTO>(todo);
        }

        public async Task<bool> Delete(long id)
        {
            var todoItem = await _repository.Get(id);

            if (todoItem == null)
            {
                return false;
            }
            await _repository.Delete(todoItem);

            return true;
        }
    }
}
