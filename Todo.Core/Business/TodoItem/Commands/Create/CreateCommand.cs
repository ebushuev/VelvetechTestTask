using MediatR;
using Todo.Core.Business.TodoItem.Dto;

namespace Todo.Core.Business.TodoItem.Commands.Create
{
    public class CreateCommand : IRequest<TodoItemDto>
    {
        public string Name { get; set; }
    }
}
