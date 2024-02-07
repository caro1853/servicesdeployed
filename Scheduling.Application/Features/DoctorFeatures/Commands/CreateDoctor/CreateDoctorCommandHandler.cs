using System;
using AutoMapper;
using MediatR;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Features.DoctorFeatures.Commands.CreateDoctor
{
	public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, int>
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IMapper mapper;

        public CreateDoctorCommandHandler(IDoctorRepository doctorRepository,
			IMapper mapper
			)
		{
            this.doctorRepository = doctorRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = new Doctor();
            doctor.Name = request.Name;

            var newDoctor = await doctorRepository.AddAsync(doctor);
            return newDoctor.Id;
        }
    }
}

