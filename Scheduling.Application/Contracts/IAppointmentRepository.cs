using System;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Contracts.Persistence
{
	public interface IAppointmentRepository : IAsyncRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int doctorId, DateTime date);
    }
}

