namespace TodoApi.Database.Models
{
    public record TodoItemDbo(long Id,
        string Name,
        bool IsComplete,
        string Secret);
}