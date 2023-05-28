using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ToDo.DeleteTodoItem
{
    public class DeleteTodoItemQuery: IRequest
    {
        public long TodoItemId { get; set; }
    }
}
