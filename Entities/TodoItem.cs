namespace TodoApiDTO.Entities
{
    #region snippet
    public class TodoItem : BaseEntity
    {
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
    #endregion
}