using System;

namespace Todo.Common.Models.DTO
{
    public class ItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}