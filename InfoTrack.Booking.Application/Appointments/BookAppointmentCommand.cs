using InfoTrack.Booking.Domain.DTOs;
using MediatR;

namespace InfoTrack.Booking.Application.Appointments;

public class BookAppointmentCommand : AppointmentDTO, IRequest<BookAppointmentCommandResponse>;
