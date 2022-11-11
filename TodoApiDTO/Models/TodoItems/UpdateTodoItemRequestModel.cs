namespace TodoApiDTO.Models.TodoItems
{
    public class UpdateTodoItemRequestModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
