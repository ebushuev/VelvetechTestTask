using Domain.Entities;

namespace Application.TodoItems.Models;

public class TodoItemShortModel
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public bool IsComplete { get; set; }

    public TodoItemShortModel()
    {}

    public TodoItemShortModel(TodoItem todoItem)
    {
        Id = todoItem.Id;
        Name = todoItem.Name;
        IsComplete = todoItem.IsComplete;
    }
}