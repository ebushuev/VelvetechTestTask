namespace TodoApi.Controllers.Todo.Models;

public record TodoItemDto(
    long Id,
    string Name,
    bool IsComplete);