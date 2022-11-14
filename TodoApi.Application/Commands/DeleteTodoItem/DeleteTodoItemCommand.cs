using MediatR;

namespace TodoApi.Application.Commands.DeleteTodoItem
{
    public sealed record DeleteTodoItemCommand(long Id) : IRequest<bool>
    {
        public long Id { get; set; } = Id;
    }
}
