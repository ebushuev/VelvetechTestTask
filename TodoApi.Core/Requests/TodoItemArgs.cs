
namespace TodoApi.Core.Requests
{
    public class TodoItemArgs
    {
        /// <summary>
        /// Todo name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Todo item readiness flag
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
