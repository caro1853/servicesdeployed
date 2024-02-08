using System;
using AutoMapper;
using Scheduling.Application.Features.AppointmentFeatures.Queries.GetAppointmentByDoctorAndDate;
using Scheduling.Application.Features.AppoitmentFeatures.Commands.CreateAppoitment;
using Scheduling.Application.Features.Shared;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OperationalHour, OperationalHoursVM>();
            CreateMap<OperationalHoursVM, OperationalHour>();
            CreateMap<Hour, HourVM>().ReverseMap();
            CreateMap<CreateAppointmentCommand, Appointment>().ReverseMap();
            CreateMap<Appointment, AppointmentMV>().ReverseMap();
            CreateMap<Patient, PatientMV>().ReverseMap();
            CreateMap<Doctor, DoctorVM>().ReverseMap();
        }

        private List<Hour> MapHoursMVToEntity(OperationalHoursVM oMV, OperationalHour oEntity)
        {
            var res = new List<Hour>();

            if (oEntity.Hours != null)
            {
                res = oEntity.Hours;
            }

            if (oMV.Hours != null)
            {
                foreach (var item in oMV.Hours)
                {
                    var e = res.Where(p => p.Schedule == item.Schedule).FirstOrDefault();
                    if (e != null)
                    {
                        e.Available = item.Available;
                    }
                    else
                    {
                        res.Add(new Hour()
                        {
                            Schedule = item.Schedule,
                            Available = item.Available
                        });
                    }
                }
            }

            return res;
        }

    }
}

