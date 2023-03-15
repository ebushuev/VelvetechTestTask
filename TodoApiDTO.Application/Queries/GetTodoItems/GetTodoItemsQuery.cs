using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApiDTO.Application.Queries.GetTodoItems
{
    public class GetTodoItemsQuery : IRequest<IEnumerable<TodoItemViewModel>>
    {
    }
}
