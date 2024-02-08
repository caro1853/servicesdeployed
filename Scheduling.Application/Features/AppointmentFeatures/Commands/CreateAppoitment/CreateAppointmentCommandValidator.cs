using System;
using FluentValidation;

namespace Scheduling.Application.Features.AppoitmentFeatures.Commands.CreateAppoitment
{
	public class CreateAppointmentCommandValidator: AbstractValidator<CreateAppointmentCommand>
    {
		public CreateAppointmentCommandValidator()
		{
		}
	}
}

