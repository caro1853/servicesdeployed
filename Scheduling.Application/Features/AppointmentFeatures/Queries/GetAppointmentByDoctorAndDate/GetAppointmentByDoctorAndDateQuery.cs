using System;
using MediatR;
using Scheduling.Application.Features.Shared;

namespace Scheduling.Application.Features.AppointmentFeatures.Queries.GetAppointmentByDoctorAndDate
{
	public class GetAppointmentByDoctorAndDateQuery: IRequest<List<AppointmentMV>>
    {
		public GetAppointmentByDoctorAndDateQuery(int doctorId, DateTime scheduleDate)
		{
            DoctorId = doctorId;
            this.ScheduleDate = scheduleDate;
        }

        public int DoctorId { get; }
        public DateTime ScheduleDate { get; }
    }
}

