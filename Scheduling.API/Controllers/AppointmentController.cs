using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Features.AppointmentFeatures.Queries.GetAppointmentByDoctorAndDate;
using Scheduling.Application.Features.AppoitmentFeatures.Commands.CreateAppoitment;
using Scheduling.Domain.Entities;

namespace Scheduling.API.Controllers
{
    [ApiController]
    [Route("api/v1/appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator mediator;

        public AppointmentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "CreateAppointment")]
        public async Task<ActionResult<int>> CreateAppointment([FromBody] CreateAppointmentCommand appointment)
        {
            try
            {
                var result = await mediator.Send(appointment);
                return result;
            }
            catch (ApplicationException ex)
            {
                var message = new
                {
                    message = ex.Message
                };
                return BadRequest(message);
            }
            catch (Exception ex)
            {
                var message = new
                {
                    message = ex.Message
                };
                return BadRequest(message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<AppointmentMV>>> GetAppointmentByDoctorAndDate(int doctorId, DateTime scheduleDate)
        {
            var query = new GetAppointmentByDoctorAndDateQuery(doctorId, scheduleDate);
            return await mediator.Send(query);
        }
    }
}

