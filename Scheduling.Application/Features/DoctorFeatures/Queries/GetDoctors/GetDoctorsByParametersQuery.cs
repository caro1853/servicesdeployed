using System;
using MediatR;
using Scheduling.Application.Features.Shared;

namespace Scheduling.Application.Features.DoctorFeatures.Queries.GetDoctors
{
	public class GetDoctorsByParametersQuery : IRequest<List<DoctorVM>>
    {
		public GetDoctorsByParametersQuery()
		{

		}
	}
}

