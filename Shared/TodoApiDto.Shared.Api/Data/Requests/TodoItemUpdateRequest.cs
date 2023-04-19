namespace TodoApiDto.Shared.Api.Data.Requests
{
    /// <summary>
    /// Model of request to update TODOItem
    /// </summary>
    public class TodoItemUpdateRequest
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