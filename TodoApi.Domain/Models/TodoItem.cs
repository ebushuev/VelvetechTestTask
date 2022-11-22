namespace TodoApi.Domain.Models
{
    #region snippet
    public class TodoItem : IDomainModel<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
    #endregion
}