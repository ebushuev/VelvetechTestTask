namespace TodoApi.Core;

public record TodoItem(
    long Id,
    string Name,
    bool IsComplete);