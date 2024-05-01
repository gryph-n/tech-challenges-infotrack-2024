using InfoTrack.Booking.Domain.Entities;
using InfoTrack.Booking.Infra.DataServices;
using InfoTrack.Booking.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrack.Booking.Application.Configuration;

public static class ServiceConfiguration
{
    public static void AddInfraServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped<IRepository<Appointment>, AppointmentRepository>();
        services.AddScoped<IAppointmentService, AppointmentService>();
    }
}
