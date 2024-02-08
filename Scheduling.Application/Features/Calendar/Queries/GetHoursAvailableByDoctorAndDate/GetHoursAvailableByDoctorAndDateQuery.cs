using System;
using MediatR;
using Scheduling.Application.Features.Shared;

namespace Scheduling.Application.Features.Calendar.Queries.GetHoursAvailableByDoctorAndDate
{
	public class GetHoursAvailableByDoctorAndDateQuery : IRequest<List<HourVM>>
    {
       

        public GetHoursAvailableByDoctorAndDateQuery(int doctorId, DateTime ScheduleDate)
		{
            DoctorId = doctorId;
            this.ScheduleDate = ScheduleDate;
        }

        public int DoctorId { get; }
        public DateTime ScheduleDate { get; }
    }
}

