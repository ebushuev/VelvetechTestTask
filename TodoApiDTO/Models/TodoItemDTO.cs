using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    #region snippet
    /// <summary>
    /// An item in a todo list
    /// </summary>
    public class TodoItemDTO
    {
        /// <summary>
        /// Item identifier in storage
        /// </summary>
        /// <example>1</example>
        public long Id { get; set; }

        /// <summary>
        /// Todo description
        /// </summary>
        /// <example>Add Swagger to the API</example>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Whether todo item is completed
        /// </summary>
        /// <example>false</example>
        [DefaultValue(false)]
        public bool IsComplete { get; set; }
    }
    #endregion
}
