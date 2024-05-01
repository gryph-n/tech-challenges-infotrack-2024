using InfoTrack.Booking.Application.Appointments;
using InfoTrack.Booking.Application.Configuration;
using InfoTrack.Booking.Tests.Fixtures;

namespace InfoTrack.Booking.Tests.Validators
{
	public class BookAppointmentCommandValidatorTests : BaseTestFixture
    {
		private readonly OpeningHoursOptions ValidOpeningHoursOptions = new() { Open = "09:00", Close = "17:00" };
        private readonly BookingsOptions ValidBookingsOptions = new() { LengthMins = 59 };

        [Theory]
		[InlineData("09:00", "John", true)]
		[InlineData("16:00", "John", true)]
		[InlineData("08:00", "John", false)] // before open
		[InlineData("17:00", "John", false)] // too late
		[InlineData("24:00", "John", false)] // invalid time
		[InlineData("", "John", false)] // missing time
		[InlineData("09:00", "", false)] // missing name
		public void Should_Validate(string bookingTime, string name, bool expectToValidate)
		{
			// Arrange
			var command = new BookAppointmentCommand { BookingTime = bookingTime, Name = name };
			var openingHoursOptions = SetupOptions(ValidOpeningHoursOptions);
			var bookingsOptions = SetupOptions(ValidBookingsOptions);
			var validator = new BookAppointmentCommandValidator(openingHoursOptions, bookingsOptions);

			// Act
			var result = validator.Validate(command);

			// Assert
			Assert.Equal(result.IsValid, expectToValidate);
		}
	}
}

