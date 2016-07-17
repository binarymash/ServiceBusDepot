using AutoMapper;

namespace ServiceBusDepot.Core.Features.ServiceBusConnection
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Entities.ServiceBusConnection, Index.Model>();                
            CreateMap<Create.Command, Entities.ServiceBusConnection>();
            CreateMap<Microsoft.ServiceBus.Messaging.QueueDescription, Details.Queue> ();
        }
    }
}
