using MediatR;

namespace Todo.BL.UseCases.TodoItems.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommandRequest : IRequest
    {
        public long Id { get; }

        public DeleteTodoItemCommandRequest(in long id)
        {
            this.Id = id;
        }
    }
}