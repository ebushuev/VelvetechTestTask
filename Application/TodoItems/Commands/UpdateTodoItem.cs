using Application.TodoItems.Models;
using Domain.Entities;
using MediatR;

namespace Application.TodoItems.Commands;

public class UpdateTodoItem : IRequest<TodoItem>
{
    public long Id { get; set; }
    
    public string Name { get; set; }

    public bool IsComplete { get; set; }

    public UpdateTodoItem()
    {}

    public UpdateTodoItem(UpdateTodoItemRequest request)
    {
        Id = request.Id;
        Name = request.Name;
        IsComplete = request.IsComplete;
    }
}