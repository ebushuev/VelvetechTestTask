using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApiDto.Application.Implementations.Command.DeleteTodoItem
{
    public class DeleteTodoItemCommand: IRequest
    {
        public Guid Id { get; set; }
        public DeleteTodoItemCommand(Guid id)
        {
            this.Id = id;
        }
    }
}
