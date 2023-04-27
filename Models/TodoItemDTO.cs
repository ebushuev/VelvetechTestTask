namespace TodoApiDTO.Models;

public class TodoItemModel
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsComplete { get; set; }
}

