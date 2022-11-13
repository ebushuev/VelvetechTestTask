
namespace TodoApi.Core.DTOs
{
    public class TodoItemDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Todo item name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Todo item readiness flag
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
