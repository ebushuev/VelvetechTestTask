namespace WebApi.Middleware;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionMiddleware(this WebApplication app) 
    { 
        app.UseMiddleware<ExceptionMiddleware>(); 
    }
}