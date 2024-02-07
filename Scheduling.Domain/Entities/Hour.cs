using System;
namespace Scheduling.Domain.Entities
{
    public class Hour
    {
        public int Id { get; set; }
        public int OperationalHourId { get; set; }
        public OperationalHour OperationalHour { get; set; }
        public int Schedule { get; set; }
        public bool Available { get; set; }
    }
}

