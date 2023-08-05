namespace Todo.Dtos;

public record TodoItemDto(Guid Id, string Name, bool IsComplete);