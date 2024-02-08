using System;
using FluentValidation;

namespace Scheduling.Application.Features.PatientFeatures.Commands.CreatePatient
{
	public class CreatePatientValidator : AbstractValidator<CreatePatientCommand>
    {
		public CreatePatientValidator()
		{
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{Name} must not exceed 100 characters.");
        }
	}
}

