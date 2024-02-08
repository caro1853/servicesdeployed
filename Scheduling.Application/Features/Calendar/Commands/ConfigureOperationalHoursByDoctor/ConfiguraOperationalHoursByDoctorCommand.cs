using System;
using MediatR;
using Scheduling.Application.Features.Shared;

namespace Scheduling.Application.Features.Calendar.Commands.ConfigureOperationalHoursByDoctor
{
	public class ConfiguraOperationalHoursByDoctorCommand: IRequest
    {
		public int DoctorId { get; set; }
		public List<OperationalHoursVM> OperationalHours { get; set; }
	}
}

