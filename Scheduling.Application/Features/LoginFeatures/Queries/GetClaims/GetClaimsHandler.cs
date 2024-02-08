using System;
using System.Security.Claims;
using MediatR;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Application.Features.DoctorFeatures.Queries.GetDoctors;
using Scheduling.Application.Features.Shared;
using Scheduling.Domain.Entities;

namespace Scheduling.Application.Features.LoginFeatures.Queries.GetClaims
{
	public class GetClaimsHandler : IRequestHandler<GetClaimsQuery, List<Claim>>
    {
        private readonly IDoctorRepository doctorRepository;
        private readonly IPatientRepository patientRepository;

        public GetClaimsHandler(IDoctorRepository doctorRepository,
            IPatientRepository patientRepository)
		{
            this.doctorRepository = doctorRepository;
            this.patientRepository = patientRepository;
        }

        public async Task<List<Claim>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
        {
            var claims = new List<Claim>();
            Patient patient = new Patient();
            //
            var doctor = await doctorRepository.GetDoctorByEmail(request.Email);
            if (doctor.Id != 0)
            {
                claims.Add(new Claim("isdoctor", "true"));
                claims.Add(new Claim("doctorid", doctor.Id.ToString()));
            }
            else
            {
                patient = await patientRepository.GetPatientByEmail(request.Email);
                if (patient.Id != 0)
                {
                    claims.Add(new Claim("ispatient", "true"));
                    claims.Add(new Claim("patientid", patient.Id.ToString()));
                }
                else
                {
                    throw new ApplicationException("Usuario inválido");
                }
            }

            return claims;

        }
    }
}

