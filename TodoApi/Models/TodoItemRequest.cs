namespace TodoApiDTO.Models
{
    public class TodoItemRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
