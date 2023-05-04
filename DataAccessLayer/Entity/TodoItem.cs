using TodoApiDTO.DataAccessLayer.Entity;

namespace TodoApi.DataAccessLayer
{
    #region snippet
    public class TodoItem : EntityBase
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
    #endregion
}