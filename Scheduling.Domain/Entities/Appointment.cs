using System;
using Scheduling.Domain.Common;
using System.Numerics;

namespace Scheduling.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int ScheduleTime { get; set; }
    }
}

