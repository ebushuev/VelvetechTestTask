using Application.TodoItems.Models;
using Domain.Entities;
using MediatR;

namespace Application.TodoItems.Commands;

public class CreateTodoItem : IRequest<TodoItem>
{
    public string Name { get; set; }

    public bool IsComplete { get; set; }

    public string Secret { get; set; }

    public CreateTodoItem() 
    {}

    public CreateTodoItem(CreateTodoItemRequest request)
    {
        Name = request.Name;
        IsComplete = request.IsComplete;
    }
}