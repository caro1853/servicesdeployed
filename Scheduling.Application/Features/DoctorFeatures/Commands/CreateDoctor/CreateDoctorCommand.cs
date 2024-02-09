using System;
using MediatR;

namespace Scheduling.Application.Features.DoctorFeatures.Commands.CreateDoctor
{
	public class CreateDoctorCommand : IRequest<int>
    {
		public string Name { get; set; }
        public string Email { get; set; }
        public string? Especiality { get; set; }
        public string? Description { get; set; }
    }
}

