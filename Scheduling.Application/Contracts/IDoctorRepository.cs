using System;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Contracts.Persistence
{
	public interface IDoctorRepository: IAsyncRepository<Doctor>
    {
        Task<Doctor> GetDoctorByEmail(string email);
    }
}

