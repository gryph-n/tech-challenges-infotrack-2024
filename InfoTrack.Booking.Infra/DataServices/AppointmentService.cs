using InfoTrack.Booking.Domain.Entities;
using InfoTrack.Booking.Domain.Helpers;
using InfoTrack.Booking.Infra.Repositories;

namespace InfoTrack.Booking.Infra.DataServices;

public interface IAppointmentService
{
    public Appointment CreateAppointment(string bookingTime, string name);
    public IEnumerable<Appointment> FindAppointmentsForTime(int bookingHour);
}

public class AppointmentService(IRepository<Appointment> _repository) : IAppointmentService
{
    public Appointment CreateAppointment(string bookingTime, string name)
    {
        var newAppointment = new Appointment { BookingTime = bookingTime, Name = name };
        return _repository.Add(newAppointment);
    }

    public IEnumerable<Appointment> FindAppointmentsForTime(int bookingHour)
    {
        return _repository.GetAll().Where(x => x.BookingTime.GetHour() == bookingHour);
    }
}
