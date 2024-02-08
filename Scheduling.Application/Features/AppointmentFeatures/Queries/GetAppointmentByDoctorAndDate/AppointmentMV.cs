using System;
using Scheduling.Application.Features.Shared;

namespace Scheduling.Application.Features.AppointmentFeatures.Queries.GetAppointmentByDoctorAndDate
{
	public class AppointmentMV
	{
        public PatientMV Patient { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int ScheduleTime { get; set; }
    }
}

