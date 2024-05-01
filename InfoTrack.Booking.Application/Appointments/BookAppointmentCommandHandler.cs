using MediatR;
using InfoTrack.Booking.Infra.DataServices;
using InfoTrack.Booking.Domain.Exceptions;
using Microsoft.Extensions.Options;
using InfoTrack.Booking.Application.Configuration;
using InfoTrack.Booking.Domain.Helpers;

namespace InfoTrack.Booking.Application.Appointments;

public class BookAppointmentCommandHandler(IOptions<BookingsOptions> _options, IAppointmentService _appointmentService) : IRequestHandler<BookAppointmentCommand, BookAppointmentCommandResponse>
{
    public async Task<BookAppointmentCommandResponse> Handle(BookAppointmentCommand command, CancellationToken cancellationToken)
    {
        var existingAppointments = _appointmentService.FindAppointmentsForTime(command.BookingTime.GetHour());
        if (existingAppointments.Count() >= _options.Value.SimultaneousCapacity)
            throw new ConflictException();

        var appointment = _appointmentService.CreateAppointment(command.BookingTime, command.Name);

        return await Task.FromResult(new BookAppointmentCommandResponse { BookingId = appointment.Id });
    }
}