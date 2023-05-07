using Todo.Common.Models.DTO;

namespace Todo.Common.Models.Responses
{
    public class ItemResponse
    {
        public ItemDto Item { get; set; }
        public string Message { get; set; }
    }
}
