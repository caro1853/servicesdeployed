using System;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduling.API.Models;
using Scheduling.Application.Features.Calendar.Commands.ConfigureOperationalHoursByDoctor;
using Scheduling.Application.Features.Calendar.Queries.GetHoursAvailableByDoctorAndDate;
using Scheduling.Application.Features.Calendar.Queries.GetOperationalHoursByDoctor;
using Scheduling.Application.Features.Shared;
using Scheduling.Domain.Entities;

namespace Scheduling.API.Controllers
{
    [ApiController]
    [Route("api/v1/calendar")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CalendarController: ControllerBase
    {
        private readonly IMediator mediator;

        public CalendarController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet()]
        [Route("getoperationalhours")]
        public async Task<ActionResult<IEnumerable<OperationalHoursVM>>> Get(int doctorId)
        {
            var query = new GetOperationalHourByDoctorQuery(doctorId);
            var response = await mediator.Send(query);
            return response;
        }


        [HttpPost()]
        [Route("configureoperationalhours")]
        public async Task<ActionResult> Configure([FromBody] ConfiguraOperationalHoursByDoctorCommand operationalHoursByDoctor, int doctorId)
        {
            await mediator.Send(operationalHoursByDoctor);
            return NoContent();
        }

        [HttpGet()]
        [Route("gethoursavailable")]
        public async Task<ActionResult<IEnumerable<HourVM>>> GetHoursAvailable(int doctorId, DateTime scheduleDate)
        {
            var query = new GetHoursAvailableByDoctorAndDateQuery(doctorId, scheduleDate);
            var response = await mediator.Send(query);
            return response;
        }
    }
}

