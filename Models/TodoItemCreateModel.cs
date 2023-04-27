namespace TodoApiDTO.Models;

public class TodoItemCreateModel
{
    public string Name { get; set; } = null!;
    public string Secret { get; set; } = null!;
}