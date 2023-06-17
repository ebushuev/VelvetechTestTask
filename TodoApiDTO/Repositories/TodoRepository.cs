using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApiDTO.DTOs;
using TodoApiDTO.Repositories.Interfaces;

namespace TodoApiDTO.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;
        private readonly IMapper _mapper;

        public TodoRepository(TodoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAll()
        {
            var todos = await _context.TodoItems.ToListAsync();
            var dtos = todos.Select(todo => _mapper.Map<TodoItemDTO>(todo));
            return dtos;
        }

        public async Task<TodoItemDTO> Get(long id)
        {
            return _mapper.Map<TodoItemDTO>(await GetById(id));
        }

        public async Task<bool> Update(long id, CreateUpdateItemTodoDTO createUpdateDTO)
        {
            try
            {
                var todoItem = await GetById(id);
                if (todoItem == null)
                    return false;
                _mapper.Map(createUpdateDTO, todoItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return false;
            }
            return true;
        }
        public async Task<TodoItemDTO> Create(CreateUpdateItemTodoDTO createUpdateDTO)
        {
            var todoItem = _mapper.Map<TodoItem>(createUpdateDTO);

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return _mapper.Map<TodoItemDTO>(todoItem);
        }

        public async Task<bool> Delete(long id)
        {
            var todoItem = await GetById(id);

            if (todoItem == null)
            {
                return false;
            }
            
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task<TodoItem> GetById(long id) => await _context.TodoItems.FindAsync(id);
        private bool TodoItemExists(long id) => _context.TodoItems.Any(todo => todo.Id == id);
    }
}
