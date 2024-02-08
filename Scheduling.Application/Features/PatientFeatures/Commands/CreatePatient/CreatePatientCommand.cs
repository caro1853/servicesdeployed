using System;
using MediatR;

namespace Scheduling.Application.Features.PatientFeatures.Commands.CreatePatient
{
	public class CreatePatientCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}

