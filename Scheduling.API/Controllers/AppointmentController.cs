using System;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Features.AppointmentFeatures.Queries.GetAppointmentByDoctorAndDate;
using Scheduling.Application.Features.AppoitmentFeatures.Commands.CreateAppoitment;
using Scheduling.Domain.Entities;

namespace Scheduling.API.Controllers
{
    [ApiController]
    [Route("api/v1/appointment")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            var result = await mediator.Send(appointment);
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<List<AppointmentMV>>> GetAppointmentByDoctorAndDate(int doctorId, DateTime scheduleDate)
        {
            var query = new GetAppointmentByDoctorAndDateQuery(doctorId, scheduleDate);
            return await mediator.Send(query);
        }
    }
}

