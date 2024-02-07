using System;
using FluentValidation;

namespace Scheduling.Application.Features.DoctorFeatures.Commands.CreateDoctor
{
	public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
    {
		public CreateDoctorCommandValidator()
		{
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} is required")
                .NotNull()
                .MaximumLength(100).WithMessage("{Name} must not exceed 100 characters.");
        }
	}
}

