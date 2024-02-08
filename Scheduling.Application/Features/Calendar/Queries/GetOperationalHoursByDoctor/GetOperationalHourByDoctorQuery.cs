using System;
using MediatR;
using Scheduling.Application.Features.Shared;

namespace Scheduling.Application.Features.Calendar.Queries.GetOperationalHoursByDoctor
{
	public class GetOperationalHourByDoctorQuery: IRequest<List<OperationalHoursVM>>
	{
        internal int DoctorId { get; }

		public GetOperationalHourByDoctorQuery(int doctorId)
		{
            DoctorId = doctorId;
        }

    }
}

