using MediatR;

namespace Todo.Core.Business.TodoItem.Commands.Delete
{
    public class DeleteCommand : IRequest
    {
        public DeleteCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
