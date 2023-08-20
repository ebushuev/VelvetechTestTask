using System.ComponentModel.DataAnnotations;

namespace Todo.Core.Models.TodoItem;

public class TodoItemDtoManipulation
{
    [Required]
    public string Name { get; init; }

    [Required]
    public bool IsComplete { get; init; }
}