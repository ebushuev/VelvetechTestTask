namespace TodoApi.BusinessLayer.Models
{
    #region snippet

    /// <summary>
    /// An item in a todo list
    /// </summary>
    public class TodoItem
    {
        /// <summary>
        /// Name to use when no name is provided
        /// </summary>
        public const string DefaultName = "Unnamed";
        private string _name;

        /// <summary>
        /// Item identifier
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Todo description
        /// </summary>
        public string Name {
            get => _name;
            set => _name = !string.IsNullOrEmpty(value) ? value : DefaultName;
        }

        /// <summary>
        /// Whether todo item is completed
        /// </summary>
        public bool IsComplete { get; set; }

        public string Secret { get; }

        public TodoItem(string name, bool isComplete = false)
        {
            Name = name;
            IsComplete = isComplete;
        }
    }
    #endregion
}