using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApiDTO.Application.Commands.CreateTodoItem
{
    public class CreateTodoItemCommand : TodoItemCommand, IRequest<long>
    {
    }
}
