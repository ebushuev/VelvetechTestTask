using MediatR;
using System.Collections.Generic;
using Todo.Common.Dto;

namespace Todo.Common.Queries
{
    public class GetAllTodosQuery : IRequest<ICollection<TodoItemDto>>
    {
    }
}
