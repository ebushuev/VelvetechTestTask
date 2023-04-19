namespace TodoApiDto.Shared.Api.Data
{
    /// <summary>
    /// TODOItem Model
    /// </summary>
    public class TodoItemViewModel
    {
        /// <summary>
        /// Record id
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