namespace FilesUploaderApi.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, exception.Message),
            InvalidOperationException => (StatusCodes.Status400BadRequest, exception.Message),
            UnauthorizedAccessException => (StatusCodes.Status403Forbidden, "Access denied."),
            ArgumentException or ArgumentNullException => (StatusCodes.Status400BadRequest, exception.Message),
            NotImplementedException => (StatusCodes.Status501NotImplemented, "Missing implementation."),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };

        response.StatusCode = statusCode;

        return response.WriteAsJsonAsync(new { error = message });
    }
}