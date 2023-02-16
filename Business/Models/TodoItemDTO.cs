namespace Business.Models
{
    #region snippet
    /// <summary>
    /// TodoItem represented the data transfer object.
    /// </summary>
    public class TodoItemDto
    {
        /// <summary>
        /// Gets or sets Id of item.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name of item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value that represents whether the task has completed or not.
        /// </summary>
        public bool IsComplete { get; set; }
    }
    #endregion
}
