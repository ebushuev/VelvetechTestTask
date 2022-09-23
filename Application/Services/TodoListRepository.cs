using Application.IServices;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TodoListRepository : ITodoListRepository
    {
        private TodoContext _todoListContext;

        public TodoListRepository(TodoContext todoListContext)
        {
            _todoListContext = todoListContext;
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItem()
        {
            return await _todoListContext.TodoItems.Select(x => ItemToDTO(x)).ToListAsync();
        }

        public async Task<TodoItemDTO> GetTodoItem(long id)
        {
            var todoItem = await _todoListContext.TodoItems.FindAsync(id);

            return ItemToDTO(todoItem);
        }

        public async Task<bool> UpdateTodoItem(TodoItemDTO todoItemDTO)
        {
            //var todoItem = new TodoItem
            //{
            //    Id = todoItemDTO.Id,
            //    IsComplete = todoItemDTO.IsComplete,
            //    Name = todoItemDTO.Name,
            //};

            var todoItem = await _todoListContext.TodoItems.FindAsync(todoItemDTO.Id);

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            _todoListContext.Update(todoItem);
            return await Save();
        }

        public async Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                Id = todoItemDTO.Id,
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name,
            };
            _todoListContext.AddAsync(todoItem);
            
            bool result = await Save();

            if (result)
            {
                return new TodoItemDTO { Id = todoItem.Id, IsComplete = todoItem.IsComplete, Name = todoItem.Name };

            }
            return null;
        }

        public async Task<bool> DeleteTodoItem(TodoItem todoItem)
        {
            _todoListContext.Remove(todoItem);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _todoListContext.SaveChangesAsync();
            return saved >= 0;
        }
    }
}