using MediatR;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Reflection;
using System;

namespace ServiceBusDepot.ConsoleHost.Configuration
{
    public class MediatrConfiguration : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IMediator, MediatR.Mediator>(new ContainerControlledLifetimeManager());
            Container.RegisterInstance<SingleInstanceFactory>(t => Container.Resolve(t));
            Container.RegisterInstance<MultiInstanceFactory>(t => Container.ResolveAll(t));

            RegisterMediatorHandlers(typeof(Application).Assembly);
            RegisterMediatorHandlers(typeof(ServiceBusDepot.Core.Features.ServiceBusConnection.MappingProfile).Assembly);
        }

        public void RegisterMediatorHandlers(Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes
                .Select(t => t.GetTypeInfo())
                .Where(IsConcreteClass);

            foreach (var classType in classTypes)
            {
                var interfaces = classType.ImplementedInterfaces
                    .Select(i => i.GetTypeInfo());

                foreach (var handlerType in interfaces.Where(IsRequestHandler))
                {
                    Container.RegisterType(handlerType.AsType(), classType.AsType());
                }

                foreach (var handlerType in interfaces.Where(IsAsyncRequestHandler))
                {
                    Container.RegisterType(handlerType.AsType(), classType.AsType());
                }
            }
        }

        public static bool IsConcreteClass(TypeInfo typeInfo)
        {
            return typeInfo.IsClass && !typeInfo.IsAbstract;
        }

        public static bool IsRequestHandler(TypeInfo typeInfo)
        {
            return
                typeInfo.IsGenericType &&
                typeInfo.GetGenericTypeDefinition() == typeof(IRequestHandler<,>);
        }

        public static bool IsAsyncRequestHandler(TypeInfo typeInfo)
        {
            return
                typeInfo.IsGenericType &&
                typeInfo.GetGenericTypeDefinition() == typeof(IAsyncRequestHandler<,>);
        }
    }
}
