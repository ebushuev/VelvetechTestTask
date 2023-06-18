using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.ApiConstans;
using TodoApiDTO.DTOs;
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
            return todos.Select(todo => _mapper.Map<TodoItemDTO>(todo));
        }

        public async Task<TodoItemDTO> Get(long id)
        {
            return _mapper.Map<TodoItemDTO>(await _repository.Get(id));
        }

        public async Task<ApiResponseStatus> Update(long id, CreateUpdateItemTodoDTO createUpdateDTO)
        {            
            var todoItem = _mapper.Map<TodoItem>(createUpdateDTO);

            return await _repository.Update(id, todoItem);
        }

        public async Task<TodoItemDTO> Create(CreateUpdateItemTodoDTO createUpdateDTO)
        {
            var todoItem = _mapper.Map<TodoItem>(createUpdateDTO);

            return _mapper.Map<TodoItemDTO>(await _repository.Create(todoItem));
        }

        public async Task<ApiResponseStatus> Delete(long id)
        {
            return await _repository.Delete(id);
        }
    }
}
