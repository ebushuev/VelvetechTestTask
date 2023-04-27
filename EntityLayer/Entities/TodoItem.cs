using TodoApi.EntityLayer.Entities.Abstract;

namespace TodoApi.EntityLayer.Entities
{
    #region snippet
    public class TodoItem : BaseEntity
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
    #endregion
}