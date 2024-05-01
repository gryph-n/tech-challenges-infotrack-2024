namespace InfoTrack.Booking.Application.Configuration;

public class BookingsOptions
{
	public int LengthMins { get; set; } = 59;
	public int SimultaneousCapacity { get; set; } = 1;
}

