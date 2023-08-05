namespace Todo.Exceptions.TodoItem;

public class TodoItemNotFoundException : NotFoundException
{
	public TodoItemNotFoundException(Guid todoItemId)
		: base($"TodoItem with id: {todoItemId} doesn't exist in the database.")
	{
	}
}
