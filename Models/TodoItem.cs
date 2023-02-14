using TodoApiDTO.Core.DataAccess;


namespace TodoApi.Models
{
    public class TodoItem : BaseEntity
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
}