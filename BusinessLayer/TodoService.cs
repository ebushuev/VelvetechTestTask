using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.Core.DataAccess;
using TodoApiDTO.Core.Services;
using TodoApiDTO.Dtos;


namespace TodoApiDTO.BusinessLayer
{
    public class TodoService : ITodoService
    {
        private readonly IRepository<TodoItem> _repository;
        private readonly IMapper _mapper;

        public TodoService(IRepository<TodoItem> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync()
        {
            var items = await _repository.GetAllAsync();

            // TODO: some business code

            var todoItemDTOs = _mapper.Map<IEnumerable<TodoItemDTO>>(items);

            return todoItemDTOs;
        }


        public async Task<TodoItemDTO> GetTodoItemByIdAsync(long id)
        {
            var todoItem = await _repository.GetByIdAsync(id);

            var todoDto = _mapper.Map<TodoItemDTO>(todoItem);

            return todoDto;
        }


        public async Task UpdateTodoItemAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = _mapper.Map<TodoItem>(todoItemDTO);

            await _repository.UpdateAsync(todoItem);
        }


        public async Task<TodoItem> CreateTodoItemAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = _mapper.Map<TodoItem>(todoItemDTO);

            return await _repository.AddAsync(todoItem);
        }


        public TodoItemDTO ItemToDTO(TodoItem todoItem)
        {
            return _mapper.Map<TodoItemDTO>(todoItem);
        }


        public async Task DeleteTodoItemAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = _mapper.Map<TodoItem>(todoItemDTO);
            await _repository.DeleteAsync(todoItem);
        }
    }
}