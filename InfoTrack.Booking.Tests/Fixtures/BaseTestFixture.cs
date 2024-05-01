using AutoFixture;
using Microsoft.Extensions.Options;

namespace InfoTrack.Booking.Tests.Fixtures;

public class BaseTestFixture
{
    internal readonly Fixture Fixture = new();

    internal IOptions<TOptions> SetupOptions<TOptions>(TOptions options) where TOptions : class
    {
        return Options.Create(options ?? Fixture.Create<TOptions>());
    }

}
