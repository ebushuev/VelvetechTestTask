using MediatR;
using TodoApi.Application.Dto;

namespace TodoApi.Application.Queries.GetTodoItem
{
    public sealed record GetTodoItemQuery(long Id) : IRequest<TodoItemDto>
    {
        public long Id { get; set; } = Id;
    }
}
