using System;
using AutoMapper;
using MediatR;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Application.Features.Shared;
using Scheduling.Application.Helpers;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Features.Calendar.Commands.ConfigureOperationalHoursByDoctor
{
	public class ConfiguraOperationalHoursByDoctorCommandHandler: IRequestHandler<ConfiguraOperationalHoursByDoctorCommand>
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IOperationalHourRepository operationalHourRepository;
        private readonly IMapper mapper;

        public ConfiguraOperationalHoursByDoctorCommandHandler(IDoctorRepository doctorRepository,
            IOperationalHourRepository operationalHourRepository,
            IMapper mapper)
		{
            this.doctorRepository = doctorRepository;
            this.operationalHourRepository = operationalHourRepository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(ConfiguraOperationalHoursByDoctorCommand request, CancellationToken cancellationToken)
        {
            var listDB = await this.operationalHourRepository.GetOperationalHoursByDoctor(request.DoctorId);

            if (listDB.Count() == 0)
            {
                foreach (var item in request.OperationalHours)
                {
                    OperationalHour operationalHour = mapper.Map<OperationalHour>(item);
                    operationalHour.DoctorId = request.DoctorId;
                    await this.operationalHourRepository.AddAsync(operationalHour);
                }
            }
            else
            {
                foreach (var item in request.OperationalHours)
                {
                    var op = listDB.FirstOrDefault(p => p.Day == item.Day);
                    if (op != null)
                    {
                        HelpersMapping.MapMVToEntity(item, op);
                        await this.operationalHourRepository.UpdateAsync(op);
                    }
                }
            }
            return Unit.Value;
        }
    }
}

