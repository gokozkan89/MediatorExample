using System;
using MediatorExample.CustomMediator;
using MediatorExample.Models.Auth;
using Microsoft.AspNetCore.Mvc;

namespace MediatorExample.Controllers.Auth
{
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

		[HttpPost("api/auth/login")]
		public Task<LoginResponse> Login([FromBody]LoginRequest request,CancellationToken cancellationToken)
        {
			return _mediator.SendAsync(request,cancellationToken);
        }
	}
}

