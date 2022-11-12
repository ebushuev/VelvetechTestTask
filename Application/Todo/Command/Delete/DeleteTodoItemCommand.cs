using MediatR;
using System;

namespace Application.Todo.Command.Delete
{
    public class DeleteTodoItemCommand: IRequest
    {
        public Guid Id { get; set; }
    }
}
