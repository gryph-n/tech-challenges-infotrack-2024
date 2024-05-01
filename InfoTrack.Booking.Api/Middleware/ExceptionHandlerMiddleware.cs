using System.Text.Json;
using InfoTrack.Booking.Api.Exceptions;

namespace InfoTrack.Booking.Api.Middleware;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ApiException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, ApiException exception)
    {
        var result = JsonSerializer.Serialize(new { error = exception.Message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception.StatusCode;

        return context.Response.WriteAsync(result);
    }
}