using TodoApiDTO.Core.Dtos;


namespace TodoApiDTO.Dtos
{
    public class TodoItemDTO : IDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}