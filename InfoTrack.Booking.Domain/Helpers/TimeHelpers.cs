using System.Text.RegularExpressions;

namespace InfoTrack.Booking.Domain.Helpers;

public static class TimeHelpers
{
    private static readonly Regex HourRegex = new(@"^\d{2}");

    private static readonly Regex MinutesRegex = new Regex(@"\d{2}$");

    public static int GetHour(this string? stringTime) => int.Parse(HourRegex.Match(stringTime ?? "").Value);

    public static int GetMinutes(this string? stringTime) => int.Parse(MinutesRegex.Match(stringTime ?? "").Value);
}

