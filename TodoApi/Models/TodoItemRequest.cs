namespace TodoApiDTO.Models
{
    /// <summary>
    /// Represents the request with todo item.
    /// </summary>
    public class TodoItemRequest
    {
        /// <summary>
        /// Gets or sets the name of element.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value that represents whether the task has completed or not.
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
