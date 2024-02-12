using System;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Features.PatientFeatures.Commands.CreatePatient;

namespace Scheduling.API.Controllers
{
    [ApiController]
    [Route("api/v1/patient")]
    public class PatientController : ControllerBase
    {
        private readonly IMediator mediator;

        public PatientController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "CreatePatient")]
        public async Task<ActionResult<int>> CreatePatient([FromBody] CreatePatientCommand patient)
        {
            var result = await mediator.Send(patient);
            return result;
        }
    }
}

