using InfoTrack.Booking.Api.Middleware;
using InfoTrack.Booking.Application.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add Services from references
        builder.Services.AddApplicationConfiguration(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddInfraServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlerMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}