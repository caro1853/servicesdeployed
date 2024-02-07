using System;

namespace Scheduling.Application.Features.Shared
{
	public class OperationalHoursVM
	{
        public int Day { get; set; }
        public bool Available { get; set; }
        public List<HourVM>? Hours { get; set; }
    }
}

