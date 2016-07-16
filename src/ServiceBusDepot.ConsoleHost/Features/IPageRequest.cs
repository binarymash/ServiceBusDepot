using MediatR;

namespace ServiceBusDepot.ConsoleHost.Features
{
    public interface IPageRequest : IRequest<IPageRequest>
    {
    }
}
