namespace TodoApiDTO.Shared.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message) : base(message) { }
    }
}
