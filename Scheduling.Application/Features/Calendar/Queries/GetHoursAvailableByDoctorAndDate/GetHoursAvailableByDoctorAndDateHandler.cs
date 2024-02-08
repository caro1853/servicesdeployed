using System;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Application.Features.Shared;

namespace Scheduling.Application.Features.Calendar.Queries.GetHoursAvailableByDoctorAndDate
{
	public class GetHoursAvailableByDoctorAndDateHandler: IRequestHandler<GetHoursAvailableByDoctorAndDateQuery, List<HourVM>>
    {
        private readonly IOperationalHourRepository operationalHourRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IMapper mapper;

        public GetHoursAvailableByDoctorAndDateHandler(
            IOperationalHourRepository operationalHourRepository,
            IAppointmentRepository appoitmentRepository,
            IMapper mapper)
		{
            this.operationalHourRepository = operationalHourRepository;
            this.appointmentRepository = appoitmentRepository;
            this.mapper = mapper;
        }

        public async Task<List<HourVM>> Handle(GetHoursAvailableByDoctorAndDateQuery request, CancellationToken cancellationToken)
        {
            var res = new List<HourVM>();
            var opHours = await operationalHourRepository.GetOperationalHoursByDoctor(request.DoctorId);
            var dayOfWeek = (int)request.ScheduleDate.DayOfWeek;
            var hoursOfDay = opHours.Where(p => p.Available && p.Day == dayOfWeek).Select(p => p.Hours.Where(p=>p.Available)).FirstOrDefault();

            var appointment = await appointmentRepository.GetAppointmentsByDoctor(request.DoctorId, request.ScheduleDate);
            if (hoursOfDay != null) {
                foreach (var item in hoursOfDay)
                {
                    if(!appointment.Any(p=>p.ScheduleTime == item.Schedule))
                    {
                        res.Add(mapper.Map<HourVM>(item));
                    }
                }
            }
            return res;
        }
    }
}

