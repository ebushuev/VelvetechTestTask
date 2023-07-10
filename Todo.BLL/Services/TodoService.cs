using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Todo.DAL.Models;
using Todo.DAL.Repositories;
using Todo.Domain.DTOs;
using Todo.Domain.Exceptions;

namespace Todo.BLL.Services
{
    public class TodoService
    {
        private readonly TodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public TodoService(TodoRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetItems()
        {
            var items = await _todoRepository.GetList();
            return _mapper.Map<IEnumerable<TodoItemDTO>>(items);
        }

        public async Task<TodoItemDTO> Get(long id)
        {
            var item = await _todoRepository.GetByIdAsync(id);
            return _mapper.Map<TodoItemDTO>(item);
        }

        public async Task Update(TodoItemDTO todoItemDTO)
        {
            var item = await _todoRepository.GetByIdAsync(todoItemDTO.Id);
            if (item == null)
            {
                throw new NotFoundException($"Todo item with id {todoItemDTO.Id} not found");
            }

            item.Name = todoItemDTO.Name;
            item.IsComplete = todoItemDTO.IsComplete;
            
            try
            {
                await _todoRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) when (!_todoRepository.IsExists(todoItemDTO.Id).Result)
            {
                throw new NotFoundException($"Todo item with id {todoItemDTO.Id} not found");
            }
        }
        
        public async Task<TodoItemDTO> Create(TodoItemDTO todoItemDTO)
        {
            var item = _mapper.Map<TodoItem>(todoItemDTO);
            await _todoRepository.Add(item);
            await _todoRepository.SaveChanges();
            return _mapper.Map<TodoItemDTO>(item);
        }
        
        public async Task Delete(long id)
        {
            var item = await _todoRepository.GetByIdAsync(id);
            if (item == null)
            {
                throw new NotFoundException($"Todo item with id {id} not found");
            }
            _todoRepository.Delete(item);
            await _todoRepository.SaveChanges();
        }
    }
}