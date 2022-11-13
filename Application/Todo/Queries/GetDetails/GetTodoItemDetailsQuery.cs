using MediatR;
using System;

namespace Application.Todo.Queries.GetDetails
{
    public class GetTodoItemDetailsQuery: IRequest<TodoDetailsVm>
    {
        public Guid Id { get; set; }    
    }
}
