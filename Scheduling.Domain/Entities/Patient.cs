using System;
using Scheduling.Domain.Common;

namespace Scheduling.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}

