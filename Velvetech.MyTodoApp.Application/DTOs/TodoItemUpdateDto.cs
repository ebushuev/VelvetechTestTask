namespace Velvetech.MyTodoApp.Application.DTOs
{
    public record TodoItemUpdateDto(Guid Id, string Name, bool IsComplete);
}
