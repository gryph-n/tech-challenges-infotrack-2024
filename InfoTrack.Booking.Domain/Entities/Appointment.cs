using InfoTrack.Booking.Domain.Helpers;

namespace InfoTrack.Booking.Domain.Entities;

public class Appointment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string BookingTime { get; set; }
    public required string Name { get; set; }
}
