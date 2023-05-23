namespace Velvetech.MyTodoApp.Application.DTOs
{
    public record TodoItemReadDto(Guid Id, string Name, bool IsComplete);
}
