using TodoApi.BLL.Interfaces;
using TodoApi.DAL.Entities;
using TodoApi.DAL.Extensions;
using TodoApi.DAL.Interfaces;
using TodoApi.DTO;

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
                throw new KeyNotFoundException("Item not found!");
            }
            return todoItem.ItemToDTO();
        }

        public async Task<IEnumerable<TodoItemDTO>> GetTodoItemsAsync()
        {
            var todoItems = await Database.TodoItems.GetTodoItemsAsync();

            return todoItems.Select(x => x.ItemToDTO()).ToList();
        }

        public async Task UpdateTodoItemAsync(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                throw new Exception("Different object IDs!");
            }

            var todoItem = await Database.TodoItems.GetTodoItemAsync(id);
            if (todoItem == null)
            {
                throw new KeyNotFoundException("Item not found!");
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            await Database.TodoItems.UpdateTodoItemAsync(todoItem);
        }

        public async Task<TodoItemDTO> CreateTodoItemAsync(TodoItemDTO todoItemDTO)
        {

            var todoItem = new TodoItem
            {
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete,
                Secret = string.Empty
            };

            return (await Database.TodoItems.CreateTodoItemAsync(todoItem)).ItemToDTO();
        }

        public async Task DeleteTodoItemAsync(long id)
        {
            var todoItem = await Database.TodoItems.GetTodoItemAsync(id);

            if (todoItem == null)
            {
                throw new KeyNotFoundException("Item not found!");
            }

            await Database.TodoItems.DeleteTodoItemAsync(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
