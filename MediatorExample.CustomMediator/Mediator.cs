namespace MediatorExample.CustomMediator
{
	public class Mediator : IMediator
	{
        private readonly Func<Type, object> _serviceResolver;
        private readonly IDictionary<Type, Type> _handlers;

        public Mediator(Func<Type, object> serviceResolver, IDictionary<Type, Type> handlers) 
        {
            _serviceResolver = serviceResolver;
            _handlers = handlers;
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request,CancellationToken cancellationToken = default)
        {
            var requestType = request.GetType();

            if (_handlers.TryGetValue(requestType,out var requestHandler))
            {
                var handler = _serviceResolver(requestHandler);
                var method = handler.GetType().GetMethod("HandleAsync")!;
                var response = method.Invoke(handler, new object[] { request, cancellationToken });
                
                return  await (Task<TResponse>)response!;
            }
            
            throw new Exception("Handler not found");
        }
    }
}

