using MediatR;
using Todo.Common.Dto;

namespace Todo.Common.Commands
{
    public class CreateTodoCommand : IRequest<TodoItemDto>
    {
        public CreateTodoCommand(TodoItemDto item)
        {
            Item = item;
        }

        public TodoItemDto Item { get; set; }
    }
}
