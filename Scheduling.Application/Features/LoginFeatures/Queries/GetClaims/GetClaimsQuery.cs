using System;
using MediatR;
using System.Security.Claims;

namespace Scheduling.Application.Features.LoginFeatures.Queries.GetClaims
{
	public class GetClaimsQuery: IRequest<List<Claim>>
    {
		public GetClaimsQuery(string email)
		{
            Email = email;
        }

        public string Email { get; }
    }
}

