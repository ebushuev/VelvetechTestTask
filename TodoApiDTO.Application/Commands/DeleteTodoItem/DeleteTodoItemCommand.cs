using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApiDTO.Application.Commands.DeleteTodoItem
{
    public class DeleteTodoItemCommand : IRequest
    {
        public long Id { get; set; }
    }
}
