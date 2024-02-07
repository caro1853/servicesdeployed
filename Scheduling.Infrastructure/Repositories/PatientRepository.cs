using System;
using Microsoft.EntityFrameworkCore;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Application.Features.Shared;
using Scheduling.Domain.Entities;
using Scheduling.Infrastructure.Persistence;

namespace Scheduling.Infrastructure.Repositories
{
	public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
		public PatientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
		}

        public async Task<Patient> GetPatientByEmail(string email)
        {
            return await _dbContext.Patients
                .FirstOrDefaultAsync(p => p.Email == email) ?? new Patient();   
        }
    }
}

