using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduling.Application.Features.DoctorFeatures.Commands.CreateDoctor;
using Scheduling.Application.Features.DoctorFeatures.Queries.GetDoctors;
using Scheduling.Application.Features.Shared;

namespace Scheduling.API.Controllers
{
    [ApiController]
    [Route("api/v1/doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator mediator;

        public DoctorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "CreateDoctor")]
        public async Task<ActionResult<int>> CreateDoctor([FromBody] CreateDoctorCommand doctor)
        {
            var result = await mediator.Send(doctor);
            return result;
        }

        [HttpGet(Name = "GetDoctors")]
        public async Task<ActionResult<List<DoctorVM>>> GetDoctors()
        {
            var result = await mediator.Send(new GetDoctorsByParametersQuery());
            return result;
        }
    }
}

