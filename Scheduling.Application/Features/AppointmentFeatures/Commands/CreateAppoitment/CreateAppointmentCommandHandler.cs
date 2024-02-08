using System;
using AutoMapper;
using MediatR;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Features.AppoitmentFeatures.Commands.CreateAppoitment
{
	public class CreateAppointmentCommandHandler: IRequestHandler<CreateAppointmentCommand, int>
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IMapper mapper;
        private readonly IDoctorRepository doctorRepository;
        private readonly IPatientRepository patientRepository;
        private readonly IOperationalHourRepository operationalHourRepository;

        public CreateAppointmentCommandHandler(IAppointmentRepository appointmentRepository,
            IMapper mapper,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository,
            IOperationalHourRepository operationalHourRepository
            )
		{
            this.appointmentRepository = appointmentRepository;
            this.mapper = mapper;
            this.doctorRepository = doctorRepository;
            this.patientRepository = patientRepository;
            this.operationalHourRepository = operationalHourRepository;
        }

        public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            //Validate if doctor exists
            if(await doctorRepository.GetByIdAsync(request.DoctorId) == null)
            {
                throw new ApplicationException("Doctor Inválido");
            }

            //Validate if patient exists
            if (await patientRepository.GetByIdAsync(request.PatientId) == null)
            {
                throw new ApplicationException("Paciente Inválido");
            }

            //Validate if the doctor has time available
            var opHours = await operationalHourRepository.GetOperationalHoursByDoctor(request.DoctorId);
            var dayAppoitment = (int)request.ScheduledDate.DayOfWeek;
            var spot = opHours.Where(p => p.Day == dayAppoitment && p.Available).FirstOrDefault()?
                .Hours?.Where(h => h.Available && h.Schedule == request.ScheduleTime).FirstOrDefault();

            if (spot == null)
            {
                throw new ApplicationException("No disponible");
            }
            //Validate if doctor doesn't have more appoitment at the same time
            var appoitments = await appointmentRepository.GetAppointmentsByDoctor(request.DoctorId, request.ScheduledDate);

            if(appoitments.Any(p=>p.ScheduleTime == request.ScheduleTime))
            {
                throw new ApplicationException("No disponible");
            }

            var entity = mapper.Map<Appointment>(request);
            var newEntity = await appointmentRepository.AddAsync(entity);
            return newEntity.Id;
        }
    }
}

