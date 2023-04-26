using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoCore.Data.Interfaces;
using TodoCore.DTOs;
using TodoCore.Services;

namespace TodoApplication.Services
{
    public class GetTodoItemsService : IGetTodoItemsService
    {
        private readonly ITodoItemReposytory _todoItemReposytory;
        private readonly IMapper _mapper;

        public GetTodoItemsService(ITodoItemReposytory todoItemReposytory, IMapper mapper)
        {
            _todoItemReposytory = todoItemReposytory;
            _mapper = mapper;
        }
        public async Task<List<TodoItemDTO>> GetTodoItemsAsync()
        {
            var todoItems = await _todoItemReposytory.GetAllAsync();
            return _mapper.Map<List<TodoItemDTO>>(todoItems);
        }
    }
}
