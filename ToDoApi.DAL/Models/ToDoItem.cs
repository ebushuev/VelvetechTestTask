using System.ComponentModel.DataAnnotations;

namespace TodoApi.DAL.Models
{
    #region snippet
    /// <summary>
    /// Task item class
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// task Id
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Task name
        /// </summary>
        [Required]  
        public string Name { get; set; }
        /// <summary>
        /// Is completed
        /// </summary>
        public bool IsComplete { get; set; }
        /// <summary>
        /// Secret information
        /// </summary>
        public string Secret { get; set; }
    }
    #endregion
}