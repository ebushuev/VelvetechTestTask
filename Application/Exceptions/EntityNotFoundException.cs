using System.Net;

namespace Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public static HttpStatusCode StatusCode = HttpStatusCode.NotFound;
    
    public EntityNotFoundException() : base() 
    {}
    
    public EntityNotFoundException(string message) : base(message) 
    {}
    
    public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
    {}
}