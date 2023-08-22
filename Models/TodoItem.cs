﻿namespace TodoApi.Models
{
    #region snippet
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsComplete { get; set; }
        public string Secret { get; set; } = "";

        public void Update(string name, bool isComplete)
        {
            Name = name;
            IsComplete = isComplete;
        }
    }
    #endregion
}
