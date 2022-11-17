namespace TodoApiDTO.BLL.DTOs.TodoItems
{
    public class CreateTodoItemResponseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
