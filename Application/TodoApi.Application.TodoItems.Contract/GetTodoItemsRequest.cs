using System.Collections.Generic;
using MediatR;
using TodoApi.DataLayer.Entity;

namespace TodoApi.Application.TodoItems.Contract
{
    public class GetTodoItemsRequest: IRequest<List<TodoItem>>
    {
        
    }
}