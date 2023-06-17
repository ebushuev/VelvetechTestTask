using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Todo.BL.DTOs;

namespace Todo.BL.UseCases.TodoItems.Queries.GetTodoItems
{
    public class GetTodoItemsQuery : IRequest<TodoItemDTO[]>
    {

    }
}
