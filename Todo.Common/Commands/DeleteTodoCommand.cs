using MediatR;

namespace Todo.Common.Commands
{
    public class DeleteTodoCommand : IRequest
    {
        public long Id { get; }

        public DeleteTodoCommand(long id)
        {
            Id = id;
        }
    }
}
