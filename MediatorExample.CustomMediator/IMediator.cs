using System.Threading;

namespace MediatorExample.CustomMediator;

public interface IMediator
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,CancellationToken cancellationToken);
}

