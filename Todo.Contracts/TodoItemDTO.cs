namespace Todo.Contracts
{
    #region snippet
    /// <summary>
    /// Represent todo task
    /// </summary>
    public class TodoItemDTO
    {
        /// <summary>
        /// Task id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Task name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// True if task has already completed
        /// </summary>
        public bool IsComplete { get; set; }
    }
    #endregion
}
