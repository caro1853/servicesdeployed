using System;
using Microsoft.EntityFrameworkCore;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Domain.Entities;
using Scheduling.Infrastructure.Persistence;

namespace Scheduling.Infrastructure.Repositories
{
	public class OperationalHourRepository: RepositoryBase<OperationalHour>, IOperationalHourRepository
    {
        public OperationalHourRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<OperationalHour>> GetOperationalHoursByDoctor(int doctorId)
        {
            return await _dbContext.OperationalHours
                .Where(p => p.DoctorId == doctorId)
                .Include(p=> p.Hours)
                .ToListAsync();
        }
    }
}

