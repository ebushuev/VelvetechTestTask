using TodoApiDto.StrongId;

namespace TodoApiDto.Services.Data
{
    /// <summary>
    /// Service TodoItem dto
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Record id
        /// </summary>
        public TodoId Id { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool IsComplete { get; set; }

        /// <summary>
        /// Secret
        /// </summary>
        public string Secret { get; set; }
    }
}