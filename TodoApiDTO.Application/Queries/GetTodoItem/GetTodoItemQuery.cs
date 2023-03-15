using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApiDTO.Application.Queries.GetTodoItem
{
    public class GetTodoItemQuery : IRequest<TodoItemViewModel>
    {
        public long Id { get; set; }
    }
}
