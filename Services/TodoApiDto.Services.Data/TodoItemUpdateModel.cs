using TodoApiDto.StrongId;

namespace TodoApiDto.Services.Data
{
    /// <summary>
    /// Model for updating TODOItem
    /// </summary>
    public class TodoItemUpdateModel
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
    }
}