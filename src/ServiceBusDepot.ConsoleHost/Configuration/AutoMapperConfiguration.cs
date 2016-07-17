using Microsoft.Practices.Unity;
using System.Collections.Generic;
using AutoMapper;

namespace ServiceBusDepot.ConsoleHost.Configuration
{
    public class AutoMapperConfiguration : UnityContainerExtension
    {
        protected override void Initialize()
        {
            //TODO: reflection
            var profiles = new List<Profile>
            {
                new Core.Features.ServiceBusConnection.MappingProfile(),
                new Core.Features.Queue.MappingProfile()
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
