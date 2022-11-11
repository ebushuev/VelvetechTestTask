namespace TodoApiDTO.Shared.Exceptions
{
    public class TodoItemNotFoundException : BaseException
    {
        public TodoItemNotFoundException(string message) : base(message) { }
    }
}
