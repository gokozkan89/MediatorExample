using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatorExample.CustomMediator
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services, params Type[] markers)
        {
            var handlerInfo = new Dictionary<Type, Type>();
            foreach (var marker in markers)
            {
                var assembly = marker.Assembly;
                var requests = GetClasses(assembly, typeof(IRequest<>));
                var handlers = GetClasses(assembly, typeof(IRequestHandler<,>));
                requests.ForEach(x =>
                {
                    handlerInfo[x] = handlers.FirstOrDefault(y =>
                    {
                        Type requestHandlerType = typeof(IRequestHandler<,>);
                        return x == y.GetInterface(requestHandlerType.Name)?.GetGenericArguments().First();
                    })!;
                });
                var serviceDescriptor = handlers.Select(x => new ServiceDescriptor(x, x, ServiceLifetime.Transient));
                services.TryAdd(serviceDescriptor);
            }

            services.AddSingleton<IMediator>(x => new Mediator(x.GetRequiredService, handlerInfo));
            return services;
        }

        private static List<Type> GetClasses(Assembly assembly, Type typeToMatch)
        {
            return assembly.ExportedTypes.Where(type =>
            {
                var genericInterfaceTypes = type.GetInterfaces().Where(x => x.IsGenericType).ToList();
                var implementRequestType = genericInterfaceTypes.Any(x => x.GetGenericTypeDefinition() == typeToMatch);
                return !type.IsInterface && !type.IsAbstract && implementRequestType;
            }).ToList();
        }
    }
}

