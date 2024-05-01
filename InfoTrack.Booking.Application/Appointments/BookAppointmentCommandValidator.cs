using FluentValidation;
using InfoTrack.Booking.Domain.Helpers;
using InfoTrack.Booking.Application.Configuration;
using Microsoft.Extensions.Options;

namespace InfoTrack.Booking.Application.Appointments;

public partial class BookAppointmentCommandValidator : AbstractValidator<BookAppointmentCommand>
{
    public BookAppointmentCommandValidator(IOptions<OpeningHoursOptions> _openingHoursOptions, IOptions<BookingsOptions> _bookingsOptions)
    {
        RuleFor(x => x.BookingTime)
            .NotNull()
            .NotEmpty()
            .Length(5)
            .Matches(@"^([0-1][0-9]|2[0-3]):\d{2}$")
            .WithMessage("BookingTime does not match 24 hour time in xx:xx format")
            .DependentRules(() =>
            {
                RuleFor(x => x.BookingTime)
                    .Must((bookingTime) =>
                    {
                        var openingHour = _openingHoursOptions.Value.Open.GetHour();
                        var closingHour = _openingHoursOptions.Value.Close.GetHour();
                        var bookingHour = bookingTime.GetHour();
                        return bookingHour >= openingHour && bookingHour <= closingHour;
                    })
                    .WithMessage("BookingTime is outside operating hours")
                    .Must((bookingTime) =>
                    {
                        var closingHour = _openingHoursOptions.Value.Close.GetHour();
                        var closingMinutes = _openingHoursOptions.Value.Close.GetMinutes();
                        var bookingHour = bookingTime.GetHour();
                        var bookingMinutes = bookingTime.GetMinutes();
                        var bookingLength = _bookingsOptions.Value.LengthMins;
                        return bookingHour <= closingHour - 2 || bookingHour <= closingHour - 1 && ((bookingMinutes + bookingLength) <= closingMinutes || closingMinutes == 0);
                    })
                    .WithMessage($"BookingTime must not be within {_bookingsOptions.Value.LengthMins} minutes of closing");
            });


        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty();
    }
}


