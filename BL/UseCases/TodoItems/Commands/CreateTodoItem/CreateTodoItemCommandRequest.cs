using MediatR;
using Todo.BL.DTOs;

namespace Todo.BL.UseCases.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandRequest : IRequest<TodoItemDTO>
    {
        public CreateTodoItemDTO TodoItemDto { get; }
        public CreateTodoItemCommandRequest(in CreateTodoItemDTO todoItemDto)
        {
            TodoItemDto = todoItemDto;
        }
    }
}