using MediatR;
using Todo.Core.Business.TodoItem.Dto;

namespace Todo.Core.Business.TodoItem.Queries
{
    public class GetByIdQuery : IRequest<TodoItemDto>
    {
        public GetByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
