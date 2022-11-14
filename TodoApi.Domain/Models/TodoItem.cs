using TodoApi.Domain.PartialModels;

namespace TodoApi.Domain.Models
{
    public class TodoItem : BaseModel
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}