using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.DAL.Models
{
    #region snippet
    [Table("TodoItems")]
    public class MTodoItem : BaseModel
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
    #endregion
}