using System;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Contracts.Persistence
{
	public interface IPatientRepository : IAsyncRepository<Patient>
    {
        Task<Patient> GetPatientByEmail(string email);
    }
}

