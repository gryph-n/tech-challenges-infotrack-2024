using InfoTrack.Booking.Application.Appointments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.Booking.Mvc.Controllers;

[ApiController]
[Route("[controller]")]
public class AppointmentController(IMediator Mediator) : ControllerBase
{

    [HttpPost(Name = "BookAppointment")]
    public async ValueTask<ObjectResult> BookAppointment([FromBody] BookAppointmentCommand command)
    {
        var result = await Mediator.Send(command);

        return Ok(result);
    }
}

