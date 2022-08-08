using Todo.Domain.Models;

namespace TodoApiDTO.Models
{
    public class TodoItemDTO
    {
        public long ? Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
