using AutoFixture;
using InfoTrack.Booking.Domain.Entities;
using InfoTrack.Booking.Infra.DataServices;
using InfoTrack.Booking.Infra.Repositories;
using InfoTrack.Booking.Tests.Fixtures;
using Moq;

namespace InfoTrack.Booking.Tests.Services;

public class AppointmentServiceTests : BaseTestFixture
{
    [Fact]
    public void CreateAppointment_Should_Add_Into_Repository()
    {
        // Arrange
        var appointment = Fixture.Create<Appointment>();
        var mockRepo = new Mock<IRepository<Appointment>>();
        mockRepo.Setup(x => x.Add(It.IsAny<Appointment>())).Returns(appointment);
        var service = new AppointmentService(mockRepo.Object);

        // Act
        var result = service.CreateAppointment(appointment.BookingTime, appointment.Name);

        // Assert
        mockRepo.Verify(x => x.Add(It.Is<Appointment>(x => x.BookingTime == appointment.BookingTime && x.Name == appointment.Name)), Times.Once);
    }

    [Fact]
    public void CreateAppointment_Should_GetAll_From_Repository()
    {
        // Arrange
        var appointments = Fixture.CreateMany<Appointment>(2);
        var mockRepo = new Mock<IRepository<Appointment>>();
        mockRepo.Setup(x => x.GetAll()).Returns(appointments.ToArray());
        var service = new AppointmentService(mockRepo.Object);

        // Act
        var result = service.FindAppointmentsForTime(1);

        // Assert
        mockRepo.Verify(x => x.GetAll(), Times.Once);
    }
}

