using AutoFixture;
using InfoTrack.Booking.Application.Appointments;
using InfoTrack.Booking.Mvc.Controllers;
using InfoTrack.Booking.Tests.Fixtures;
using MediatR;
using Moq;

namespace InfoTrack.Booking.Tests.Controllers;

public class AppointmentControllerTests : BaseTestFixture
{
    private readonly BookAppointmentCommand ValidBookAppointmentCommand = new() { BookingTime = "09:00", Name = "John" };

    [Fact]
    public async void Should_Return_BookingId_If_Success()
    {
        // Arrange
        var bookingId = Fixture.Create<string>();
        var mediator = SetupMediator(new BookAppointmentCommandResponse { BookingId = bookingId });
        var controller = new AppointmentController(mediator);

        // Act
        var result = await controller.BookAppointment(ValidBookAppointmentCommand);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Value);
        Assert.IsType<BookAppointmentCommandResponse>(result.Value);
        Assert.Equal(((BookAppointmentCommandResponse)result.Value).BookingId, bookingId);
    }

    [Fact]
    public async void Should_Throw_If_Mediator_Throws()
    {
        // Arrange
        var mediator = SetupMediator<BookAppointmentCommandResponse>(shouldThrow: true);
        var controller = new AppointmentController(mediator);

        // Act
        var result = async () => await controller.BookAppointment(ValidBookAppointmentCommand);

        // Assert
        await Assert.ThrowsAsync<Exception>(result);
    }

    private static IMediator SetupMediator<TResponse>(TResponse response = null, bool shouldThrow = false) where TResponse : class
    {
        var mock = new Mock<IMediator>();
        if (shouldThrow)
            mock.Setup(x => x.Send(It.IsAny<IRequest<TResponse>>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());
        else
            mock.Setup(x => x.Send(It.IsAny<IRequest<TResponse>>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);
        return mock.Object;
    }
}

