using System;
using MediatR;

namespace Scheduling.Application.Features.AppoitmentFeatures.Commands.CreateAppoitment
{
	public class CreateAppointmentCommand : IRequest<int>
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int ScheduleTime { get; set; }
    }
}

