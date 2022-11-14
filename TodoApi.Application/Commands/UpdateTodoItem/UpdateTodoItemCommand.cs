using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using TodoApi.Application.Dto;

namespace TodoApi.Application.Commands.UpdateTodoItem
{
    public sealed record UpdateTodoItemCommand(TodoItemDto Item) : IRequest<bool>
    {
        public TodoItemDto Item { get; set; } = Item;
    }
}
