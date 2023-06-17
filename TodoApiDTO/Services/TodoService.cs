using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.DTOs;
using TodoApiDTO.Repositories.Interfaces;
using TodoApiDTO.Services.Interfaces;

namespace TodoApiDTO.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<TodoItemDTO> Get(long id)
        {
            return await _repository.Get(id);
        }

        public async Task<bool> Update(long id, CreateUpdateItemTodoDTO createUpdateDTO)
        {
            return await _repository.Update(id, createUpdateDTO);
        }

        public async Task<TodoItemDTO> Create(CreateUpdateItemTodoDTO createUpdateDTO)
        {
            return await _repository.Create(createUpdateDTO);
        }

        public async Task<bool> Delete(long id)
        {
            return await _repository.Delete(id);
        }
    }
}
