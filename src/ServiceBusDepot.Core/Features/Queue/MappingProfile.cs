using AutoMapper;
using System.IO;

namespace ServiceBusDepot.Core.Features.Queue
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Microsoft.ServiceBus.Messaging.BrokeredMessage, Inspect.Message>()
                .ForMember(dest => dest.Body, opt => opt.ResolveUsing(src =>
                    {
                        return new StreamReader(src.GetBody<Stream>()).ReadToEnd();
                    }));
        }
    }
}
