using MediatR;
using TodoApi.Application.Dto;

namespace TodoApi.Application.Commands.CreateTodoItem
{
    public sealed record CreateTodoItemCommand(CreateTodoItemDto Item) : IRequest<long>
    {
        public CreateTodoItemDto Item { get; set; } = Item;
    }
}
