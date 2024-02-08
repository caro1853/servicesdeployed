using System;
using MediatR;
using Scheduling.Application.Contracts.Persistence;
using System.Linq;
using AutoMapper;
using Scheduling.Domain.Entities;
using Scheduling.Application.Features.Shared;

namespace Scheduling.Application.Features.Calendar.Queries.GetOperationalHoursByDoctor
{
	public class GetOperationalHourByDoctorHandler: IRequestHandler<GetOperationalHourByDoctorQuery, List<OperationalHoursVM>>
	{
        private readonly IDoctorRepository doctorRepository;
        private readonly IOperationalHourRepository operationalHourRepository;
        private readonly IMapper mapper;

        public GetOperationalHourByDoctorHandler(
            IDoctorRepository doctorRepository,
            IOperationalHourRepository operationalHourRepository,
            IMapper mapper
            )
		{
            this.doctorRepository = doctorRepository;
            this.operationalHourRepository = operationalHourRepository;
            this.mapper = mapper;
        }

        public async Task<List<OperationalHoursVM>> Handle(GetOperationalHourByDoctorQuery request, CancellationToken cancellationToken)
        {
            //Validate if doctor exists
            var doctor = await doctorRepository.GetByIdAsync(request.DoctorId);
            if (doctor == null)
            {
                return new List<OperationalHoursVM>();
            }

            var list = await operationalHourRepository.GetOperationalHoursByDoctor(request.DoctorId);
            if (list.Count() == 0)
            {
                list = AssignNewConfiguration(request.DoctorId);
            }
            return mapper.Map<List<OperationalHoursVM>>(list);
        }
        
        private List<OperationalHour> AssignNewConfiguration(int doctorId)
        {
            var listOperationalHours = new List<OperationalHour>();
            var dayOff = new List<int>() { 0, 6};
            var hourOff = new List<int>() { 12, 13 };
            int start = 8;
            int end = 17;
            for (int day = 0; day < 7; day++)
            {
                var op = new OperationalHour()
                {
                    DoctorId = doctorId,
                    Day = day,
                    Available = !dayOff.Contains(day)
                };
                op.Hours = new List<Hour>();
                bool available = !dayOff.Contains(day);
                for (int hour = start; hour <= end; hour++)
                {
                    op.Hours.Add(
                        new Hour
                        {
                            Schedule = hour,
                            Available = !hourOff.Contains(hour) && available
                        });
                }
                listOperationalHours.Add(op);
            }
            return listOperationalHours;
        }
    }
}

