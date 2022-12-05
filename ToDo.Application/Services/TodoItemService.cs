using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ToDo.Application.Models;
using ToDo.DAL.Repositories;
using ToDo.Domain.Models;

namespace ToDo.Application.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repository;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ToDoDto>> GetAllAsync()
        {
            var item = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ToDoDto>>(item).ToList();
        }

        public async Task<ToDoDto> GetAsync(int id)
        {
            var item = await _repository.GetAsync(id);
            return _mapper.Map<ToDoDto>(item);
        }

        public async Task<ToDoDto> CreateAsync(ToDoDto entity)
        {
            var todoItem = _mapper.Map<ToDoItem>(entity);
            var item = await _repository.CreateAsync(todoItem);

            return _mapper.Map<ToDoDto>(item);
        }

        public async Task<ToDoDto> UpdateAsync(ToDoDto entity)
        {
            var todoItem = _mapper.Map<ToDoItem>(entity);
            var item = await _repository.UpdateAsync(todoItem);

            return _mapper.Map<ToDoDto>(item);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}