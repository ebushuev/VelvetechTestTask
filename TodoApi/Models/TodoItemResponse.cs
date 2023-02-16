﻿namespace TodoApiDTO.Models
{
    /// <summary>
    /// Represents the response with todo item.
    /// </summary>
    public class TodoItemResponse
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
}
