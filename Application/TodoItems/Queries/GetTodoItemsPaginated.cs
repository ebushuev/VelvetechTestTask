using Domain.Entities;
using MediatR;

namespace Application.TodoItems.Queries;

public class GetTodoItemsPaginated : IRequest<IEnumerable<TodoItem>>
{
    public int Page { get; set; }

    public int PageSize { get; set; }

    public GetTodoItemsPaginated()
    {}

    public GetTodoItemsPaginated(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}