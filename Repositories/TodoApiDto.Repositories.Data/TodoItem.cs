using System;

namespace TodoApiDto.Repositories.Data
{
    /// <summary>
    /// Db TodoItem dto
    /// </summary>
    [Serializable]
    public class TodoItem
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

        /// <summary>
        /// Secret
        /// </summary>
        public string Secret { get; set; }
    }
}