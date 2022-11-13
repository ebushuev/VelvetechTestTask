using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApi.BLL.Interfaces;
using TodoApi.DAL.Interfaces;
using TodoApi.DAL.Models;
using TodoApi.Models;

namespace TodoApi.BLL.Services
{
    public class TodoItemService : ITodoItemService
    {
        IUnitOfWork Database { get; set; }

        public TodoItemService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<TodoItemDTO> GetTodoItemAsync(long id)
        {
            var todoItem = await Database.TodoItems.GetTodoItemAsync(id);

            if (todoItem == null)
            {
                throw new Exception("Todo item not found!");
            }
            return ItemToDTO(todoItem);
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync()
        {
            var todoItems = await Database.TodoItems.GetTodoItemsAsync();
            
            return todoItems.Select(x => ItemToDTO(x)).ToList();
        }

        public async Task UpdateTodoItemAsync(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                throw new Exception("Id and todo item it are not equal");
            }

            var todoItem = await Database.TodoItems.GetTodoItemAsync(id);
            if (todoItem == null)
            {
                throw new Exception("Todo item not found!");
            }

            await Database.TodoItems.UpdateTodoItemAsync(todoItem);
        }

        public async Task<TodoItemDTO> CreateTodoItemAsync(TodoItemDTO todoItemDTO)
        {

            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            var todoItemSaved = await Database.TodoItems.CreateTodoItemAsync(todoItem);

            return ItemToDTO(todoItemSaved);
        }

        public async Task DeleteTodoItemAsync(long id)
        {
            var todoItem = await Database.TodoItems.GetTodoItemAsync(id);

            if (todoItem == null)
            {
                throw new Exception("Todo item not found!");
            }
         
            await Database.TodoItems.DeleteTodoItemAsync(id);
        }
        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
           new TodoItemDTO
           {
               Id = todoItem.Id,
               Name = todoItem.Name,
               IsComplete = todoItem.IsComplete
           };

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
