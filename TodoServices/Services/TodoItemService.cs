using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoEntities.DbSet;
using TodoIData.IRepositiries;
using TodoIData.IServices;
using TodoModels.Models;

namespace TodoServices.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemRepository accountRepository, IMapper mapper)
        {
            _todoItemRepository = accountRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<TodoItemDTO>> GetAllAsync()
        {
            var todoItems = await _todoItemRepository.GetAllAsync();
            var todoItemDTOs = _mapper.Map<List<TodoItemDTO>>(todoItems);
            return todoItemDTOs;
        }

        public async Task<TodoItemDTO> GetByIdAsync(long id)
        {
            var todoItem = await _todoItemRepository.GetByIdAsync(id);
            var todoItemDTO = _mapper.Map<TodoItemDTO>(todoItem);
            return todoItemDTO;
        }

        public async Task AddAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = _mapper.Map<TodoItem>(todoItemDTO);
            await _todoItemRepository.AddAsync(todoItem);
        }

        public async Task UpdateAsync(TodoItemDTO todoItemDTO)
        {
            var todoItem = _mapper.Map<TodoItem>(todoItemDTO);
            await _todoItemRepository.UpdateAsync(todoItem);
        }

        public async Task DeleteAsync(long id)
        {
            await _todoItemRepository.DeleteAsync(id);
        }
    }
}
