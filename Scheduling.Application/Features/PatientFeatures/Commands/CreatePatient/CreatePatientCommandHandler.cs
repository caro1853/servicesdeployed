using System;
using AutoMapper;
using MediatR;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Application.Features.DoctorFeatures.Commands.CreateDoctor;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Features.PatientFeatures.Commands.CreatePatient
{
	public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, int>
    {
        private readonly IPatientRepository patientRepository;
        private readonly IMapper mapper;

        public CreatePatientCommandHandler(IPatientRepository patientRepository,
            IMapper mapper
            )
		{
            this.patientRepository = patientRepository;
            this.mapper = mapper;
        }

        public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = new Patient();
            patient.Name = request.Name;

            var newPatient = await patientRepository.AddAsync(patient);
            return newPatient.Id;
        }
    }
}

