using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApiDto.Domain.Entities;

namespace TodoApiDto.Application.Implementations.Queries.GetToItem
{
    public class GetToDoItemQuery: IRequest<TodoItem>
    {
        public Guid Id { get; set; }
        public GetToDoItemQuery(Guid id)
        {
            this.Id = id;
        }
    }
}
