using Microsoft.Practices.Unity;
using System.Collections.Generic;
using AutoMapper;

namespace ServiceBusDepot.ConsoleHost.Configuration
{
    public class AutoMapperConfiguration : UnityContainerExtension
    {
        protected override void Initialize()
        {
            var profiles = new List<Profile>
            {
                new ServiceBusDepot.Core.Features.ServiceBusConnection.MappingProfile()
            };

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            Container.RegisterInstance(config);
            Container.RegisterType<IMapper>(new InjectionFactory(c => c.Resolve<MapperConfiguration>().CreateMapper()));
        }
    }
}
