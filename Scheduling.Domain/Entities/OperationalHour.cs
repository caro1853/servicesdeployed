using System;
using Scheduling.Domain.Common;

namespace Scheduling.Domain.Entities
{
    public class OperationalHour : BaseEntity
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int Day { get; set; }
        public bool Available { get; set; }
        public List<Hour> Hours { get; set; }
    }
}

