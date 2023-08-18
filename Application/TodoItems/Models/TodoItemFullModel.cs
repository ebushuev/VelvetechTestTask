using Domain.Entities;

namespace Application.TodoItems.Models;

public class TodoItemFullModel
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public bool IsComplete { get; set; }
    
    public string Secret { get; set; }

    public TodoItemFullModel()
    {}

    public TodoItemFullModel(TodoItem todoItem)
    {
        Id = todoItem.Id;
        Name = todoItem.Name;
        IsComplete = todoItem.IsComplete;
        Secret = todoItem.Secret;
    }
}