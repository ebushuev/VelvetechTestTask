using System.ComponentModel.DataAnnotations;

namespace Todo.Dtos;

public abstract record TodoItemForManipulationDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    public string Name { get; init; }

    [Required(ErrorMessage = "IsComplete is a required field.")]
    public bool IsComplete { get; init; }
}
