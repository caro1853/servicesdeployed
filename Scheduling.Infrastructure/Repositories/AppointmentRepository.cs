using System;
using Microsoft.EntityFrameworkCore;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Domain.Entities;
using Scheduling.Infrastructure.Persistence;

namespace Scheduling.Infrastructure.Repositories
{
	public class AppointmentRepository : RepositoryBase<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int doctorId, DateTime date)
        {
            return await _dbContext.Appointments
                    .Where(p=> p.DoctorId == doctorId
                        && p.ScheduledDate.Date == date.Date
                    )
                    .Include(p=>p.Patient)
                    .ToListAsync();
        }
    }
}

