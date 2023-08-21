namespace TodoApi.WebApi.Dto
{
    public abstract class BaseTodoItemDto
    {
        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}
