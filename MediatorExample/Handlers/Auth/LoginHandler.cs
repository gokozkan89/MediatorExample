using MediatorExample.CustomMediator;
using MediatorExample.Models.Auth;

namespace MediatorExample.Handlers.Auth
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        public async Task<LoginResponse> HandleAsync(LoginRequest request,CancellationToken cancellationToken)
        {
            return await Task.FromResult(new LoginResponse());
        }
    }
}

