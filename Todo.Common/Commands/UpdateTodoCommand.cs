using MediatR;
using Todo.Common.Dto;

namespace Todo.Common.Commands
{
    public class UpdateTodoCommand : IRequest
    {
        public UpdateTodoCommand(TodoItemDto item)
        {
            Item = item;
        }

        public TodoItemDto Item { get; }
    }
}
