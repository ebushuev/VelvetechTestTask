namespace Data.Entities
{
    #region snippet
    /// <summary>
    /// Todo item entity.
    /// </summary>
    public class TodoItemEntity
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