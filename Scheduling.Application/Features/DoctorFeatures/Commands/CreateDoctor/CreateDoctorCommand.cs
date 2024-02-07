using System;
using MediatR;

namespace Scheduling.Application.Features.DoctorFeatures.Commands.CreateDoctor
{
	public class CreateDoctorCommand : IRequest<int>
    {
		public string Name { get; set; }
	}
}

