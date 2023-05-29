using MediatR;
using System.Collections.Generic;
using TodoApiDto.Domain.Entities;

namespace TodoApiDto.Application.Implementations.Queries.GetItems
{
    public class GetToDoItemsQuery : IRequest<IEnumerable<TodoItem>>
    {
    }
}