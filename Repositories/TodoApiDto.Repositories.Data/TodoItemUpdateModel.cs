namespace TodoApiDto.Repositories.Data
{
    /// <summary>
    /// Model for updating TODOItem
    /// </summary>
    public class TodoItemUpdateModel
    {
        /// <summary>
        /// record id
        /// </summary>
        public long Id { get; set; }

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