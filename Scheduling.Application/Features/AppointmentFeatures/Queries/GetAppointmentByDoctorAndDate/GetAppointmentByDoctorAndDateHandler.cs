using System;
using AutoMapper;
using MediatR;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Application.Features.Shared;

namespace Scheduling.Application.Features.AppointmentFeatures.Queries.GetAppointmentByDoctorAndDate
{
	public class GetAppointmentByDoctorAndDateHandler : IRequestHandler<GetAppointmentByDoctorAndDateQuery, List<AppointmentMV>>
    {
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IMapper mapper;

        public GetAppointmentByDoctorAndDateHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
		{
            this.appointmentRepository = appointmentRepository;
            this.mapper = mapper;
        }

        public async Task<List<AppointmentMV>> Handle(GetAppointmentByDoctorAndDateQuery request, CancellationToken cancellationToken)
        {
            var entities = await appointmentRepository.GetAppointmentsByDoctor(request.DoctorId, request.ScheduleDate);
            return mapper.Map<List<AppointmentMV>>(entities);
        }
    }
}

