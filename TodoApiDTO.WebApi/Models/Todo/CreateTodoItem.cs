using System.Security.Policy;

namespace TodoApiDTO.WebApi.Models.Todo
{
    public class CreateTodoItem
    {
        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}
