using Microsoft.AspNetCore.Http;

namespace InfoTrack.Booking.Api.Exceptions;

public abstract class ApiException(int statusCode = StatusCodes.Status500InternalServerError, string message = "Something went wrong") : Exception(message)
{
    public int StatusCode { get; set; } = statusCode;
}