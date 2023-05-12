using MediatR;
using Todo.Common.Dto;

namespace Todo.Common.Queries
{
    public class GetTodoQuery : IRequest<TodoItemDto>
    {
        public long Id { get; }

        public GetTodoQuery(long id)
        {
            Id = id;
        }
    }
}
