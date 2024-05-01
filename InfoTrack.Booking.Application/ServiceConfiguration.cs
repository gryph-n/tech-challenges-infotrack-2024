using FluentValidation;
using FluentValidation.AspNetCore;
using InfoTrack.Booking.Application.Appointments;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.Booking.Application.Configuration;

public static class ServiceConfiguration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddScoped<IValidator<BookAppointmentCommand>, BookAppointmentCommandValidator>();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<BookAppointmentCommand>();
        });
    }

    public static void AddApplicationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BookingsOptions>(configuration.GetSection("Bookings"));
        services.Configure<OpeningHoursOptions>(configuration.GetSection("OpeningHours"));
    }
}
