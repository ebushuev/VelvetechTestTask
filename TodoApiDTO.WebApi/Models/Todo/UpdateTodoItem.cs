namespace TodoApiDTO.WebApi.Models.Todo
{
    public class UpdateTodoItem
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool? IsComplete { get; set; }
    }
}
