using InfoTrack.Booking.Api.Exceptions;
using Microsoft.AspNetCore.Http;

namespace InfoTrack.Booking.Domain.Exceptions;

public class ConflictException(string message = "Conflict") : ApiException(StatusCodes.Status409Conflict, message);
