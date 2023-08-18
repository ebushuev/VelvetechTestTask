using Domain.Entities;
using MediatR;

namespace Application.TodoItems.Queries;

public class GetTodoItemById : IRequest<TodoItem>
{
    public long Id { get; set; }

    public GetTodoItemById()
    {}

    public GetTodoItemById(long id)
    {
        Id = id;
    }
}