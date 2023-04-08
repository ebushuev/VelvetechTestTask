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

        /// <summary>
        /// Item identifier
        /// </summary>
        public long Id { get; }

        /// <summary>
        /// Todo description
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Whether todo item is completed
        /// </summary>
        public bool IsComplete { get; private set; }

        public string Secret { get; }

        public TodoItem(string name, bool isComplete = false)
        {
            SetName(name);
            SetComplete(isComplete);
        }

        public void SetName(string name)
        {
            Name = !string.IsNullOrEmpty(name) ? name : DefaultName;
        }

        public void SetComplete(bool isComplete)
        {
            IsComplete = isComplete;
        }
    }
    #endregion
}