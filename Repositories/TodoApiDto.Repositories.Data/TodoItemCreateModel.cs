namespace TodoApiDto.Repositories.Data
{
    /// <summary>
    /// Model for creating TODOItem
    /// </summary>
    public class TodoItemCreateModel
    {
        /// <summary>
        /// Content
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool IsComplete { get; set; }
    }
}