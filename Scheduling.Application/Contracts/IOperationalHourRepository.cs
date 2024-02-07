using System;
using Scheduling.Domain.Common;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Contracts.Persistence
{
	public interface IOperationalHourRepository: IAsyncRepository<OperationalHour>
    {
		Task<IEnumerable<OperationalHour>> GetOperationalHoursByDoctor(int doctorId);
	}
}

