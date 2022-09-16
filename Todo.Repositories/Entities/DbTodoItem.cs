
namespace Velvetech.Todo.Repositories.Entities
{
  public  class DbTodoItem
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
    public string Secret { get; set; }
  }
}
