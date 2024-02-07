using System;
using AutoMapper;
using MediatR;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Application.Features.Shared;

namespace Scheduling.Application.Features.DoctorFeatures.Queries.GetDoctors
{
	public class GetDoctorsByParametersHandler : IRequestHandler<GetDoctorsByParametersQuery, List<DoctorVM>>
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IMapper mapper;

        public GetDoctorsByParametersHandler(IDoctorRepository doctorRepository,
            IMapper mapper)
		{
            this.doctorRepository = doctorRepository;
            this.mapper = mapper;
        }

        public async Task<List<DoctorVM>> Handle(GetDoctorsByParametersQuery request, CancellationToken cancellationToken)
        {
            var entities = await doctorRepository.GetAllAsync();

            return mapper.Map<List<DoctorVM>>(entities);
        }
    }
}

