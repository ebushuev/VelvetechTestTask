namespace TodoApiDto.Shared.Api.Data.Requests
{
    /// <summary>
    /// Model of request to create TODOItem
    /// </summary>
    public class TodoItemCreateRequest
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