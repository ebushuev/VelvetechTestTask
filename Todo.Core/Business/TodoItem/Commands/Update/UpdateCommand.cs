using MediatR;

namespace Todo.Core.Business.TodoItem.Commands.Update
{
    public class UpdateCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}
