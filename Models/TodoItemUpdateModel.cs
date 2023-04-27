using System;

namespace TodoApiDTO.Models;

public class TodoItemUpdateModel
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
}