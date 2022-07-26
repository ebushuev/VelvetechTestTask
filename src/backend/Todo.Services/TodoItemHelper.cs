using Todo.Domain.DataTransferObjects;
using Todo.Domain.Entities;

namespace Todo.Services
{
    public class TodoItemHelper
    {
        public static TodoItemDto ItemToDto(TodoItem todoItem) =>
           new TodoItemDto
           {
               Id = todoItem.Id,
               Name = todoItem.Name,
               IsComplete = todoItem.IsComplete
           };
    }
}