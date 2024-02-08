using System;
using Scheduling.Application.Features.Shared;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Helpers
{
    public static class HelpersMapping
    {

        public static void MapMVToEntity(OperationalHoursVM oMV, OperationalHour oEntity)
        {
            oEntity.Available = oMV.Available;

            if (oEntity.Hours == null)
            {
                oEntity.Hours = new List<Hour>();
            }

            if (oMV.Hours != null)
            {
                foreach (var item in oMV.Hours)
                {
                    var e = oEntity.Hours?.Where(p => p.Schedule == item.Schedule).FirstOrDefault();
                    if (e != null)
                    {
                        e.Available = item.Available;
                    }
                    else
                    {
                        oEntity.Hours?.Add(new Hour()
                        {
                            Schedule = item.Schedule,
                            Available = item.Available
                        });
                    }
                }
            }
        }

    }
}

