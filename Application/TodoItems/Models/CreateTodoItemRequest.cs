namespace Application.TodoItems.Models;

public class CreateTodoItemRequest
{
    public string Name { get; set; }

    public bool IsComplete { get; set; }
}