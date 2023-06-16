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
            // TODO: mapper
            //var dtos = todos.Select(todo => _mapper.Map<TodoItemDTO>(todo));
            var dtos = todos.Select(todo => ItemToDTO(todo));
            return dtos;
        }

        public async Task<TodoItemDTO> Get(long id)
        {
            var todo = await _repository.Get(id);
            // TODO: mapper
            //return _mapper.Map<TodoItemDTO>(todo);
            return ItemToDTO(todo);
        }

        public async Task<bool> Update(long id, TodoItemDTO todoItemDTO)
        {
            var todoItem = await _repository.Get(id);
            if (todoItem == null)
                return false;
            // TODO: mapper
            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;
            return await _repository.Update(id, todoItem);
        }

        public async Task<TodoItemDTO> Create(TodoItemDTO todoItemDTO)
        {
            // TODO: mapper
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            // TODO: mapper
            //return await _repository.Create(todoItem);
            var todo = await _repository.Create(todoItem);
            return ItemToDTO(todo);
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

        // TODO: remove
        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
