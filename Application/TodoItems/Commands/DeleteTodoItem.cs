using MediatR;

namespace Application.TodoItems.Commands;

public class DeleteTodoItem : IRequest
{
    public long Id { get; set; }

    public DeleteTodoItem()
    {}

    public DeleteTodoItem(long id)
    {
        Id = id;
    }
}