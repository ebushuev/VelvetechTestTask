using MediatR;
using Todo.BL.DTOs;

namespace Todo.BL.UseCases.TodoItems.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommandRequest : IRequest
    {
        public TodoItemDTO TodoItemDto { get; }
        public UpdateTodoItemCommandRequest(in TodoItemDTO todoItemDto)
        {
            TodoItemDto = todoItemDto;
        }
    }
}
