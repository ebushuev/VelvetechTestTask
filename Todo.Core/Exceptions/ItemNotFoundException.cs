namespace Todo.Core.Exceptions;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(string message) : base(message)
    {
    }

    public ErrorDetails ToErrorDetails()
    {
        return new ErrorDetails(404, Message); // 404 indicates "Not Found" status
    }
}