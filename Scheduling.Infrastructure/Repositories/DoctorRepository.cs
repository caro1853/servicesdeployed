using System;
using Microsoft.EntityFrameworkCore;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Domain.Entities;
using Scheduling.Infrastructure.Persistence;

namespace Scheduling.Infrastructure.Repositories
{
    public class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
	{
        public DoctorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Doctor> GetDoctorByEmail(string email)
        {
            return await _dbContext.Doctors
                .FirstOrDefaultAsync(p => p.Email == email) ?? new Doctor();
        }
    }
}

