using System.Collections.Generic;
using MediatR;
using TodoApi.Application.Dto;

namespace TodoApi.Application.Queries.GetTodoItemList
{
    public sealed record GetTodoItemListQuery : IRequest<IEnumerable<TodoItemDto>>
    {
    }
}
