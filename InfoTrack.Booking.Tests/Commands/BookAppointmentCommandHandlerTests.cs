using AutoFixture;
using InfoTrack.Booking.Application.Appointments;
using InfoTrack.Booking.Application.Configuration;
using InfoTrack.Booking.Domain.Entities;
using InfoTrack.Booking.Domain.Exceptions;
using InfoTrack.Booking.Infra.DataServices;
using InfoTrack.Booking.Tests.Fixtures;
using Moq;

namespace InfoTrack.Booking.Tests.Commands;

public class BookAppointmentCommandHandlerTests : BaseTestFixture
{
    private readonly BookAppointmentCommand ValidCommand = new() { BookingTime = "09:00", Name = "John" };
    private readonly BookingsOptions BookingsOptions = new() { SimultaneousCapacity = 2 };

    [Fact]
    public async void Should_Return_BookingId_If_Success()
    {
        // Arrange
        var options = SetupOptions(BookingsOptions);
        var appointmentService = new Mock<IAppointmentService>();
        appointmentService.Setup(x => x.FindAppointmentsForTime(It.IsAny<int>()))
            .Returns(Fixture.CreateMany<Appointment>(1));
        appointmentService.Setup(x => x.CreateAppointment(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(new Appointment { BookingTime = ValidCommand.BookingTime, Name = ValidCommand.Name });
        var handler = new BookAppointmentCommandHandler(options, appointmentService.Object);

        // Act
        var result = await handler.Handle(ValidCommand, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<BookAppointmentCommandResponse>(result);
        Assert.True(!string.IsNullOrEmpty(result.BookingId));
    }

    [Fact]
    public async void Should_Throw_ConflictException_If_Existing_Appointments_At_SimultaneousCapacity()
    {
        // Arrange
        var options = SetupOptions(BookingsOptions);
        var appointmentService = new Mock<IAppointmentService>();
        appointmentService.Setup(x => x.FindAppointmentsForTime(It.IsAny<int>()))
            .Returns(Fixture.CreateMany<Appointment>(3));
        var handler = new BookAppointmentCommandHandler(options, appointmentService.Object);

        // Act
        var result = async () => await handler.Handle(ValidCommand, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<ConflictException>(result);
    }

    [Fact]
    public async void Should_Throw_If_CreateAppointment_Throws()
    {
        // Arrange
        var options = SetupOptions(BookingsOptions);
        var appointmentService = new Mock<IAppointmentService>();
        appointmentService.Setup(x => x.FindAppointmentsForTime(It.IsAny<int>()))
            .Returns(Fixture.CreateMany<Appointment>(1));
        appointmentService.Setup(x => x.CreateAppointment(It.IsAny<string>(), It.IsAny<string>()))
            .Throws<Exception>();
        var handler = new BookAppointmentCommandHandler(options, appointmentService.Object);

        // Act
        var result = async () => await handler.Handle(ValidCommand, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<Exception>(result);
    }
}
