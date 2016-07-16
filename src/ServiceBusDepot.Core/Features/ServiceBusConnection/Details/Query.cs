using MediatR;

namespace ServiceBusDepot.Core.Features.ServiceBusConnection.Details
{
    public class Query : IAsyncRequest<Model>
    {
        public int ServiceBusConnectionId { get; private set; }

        public Query(int serviceBusConnectionId)
        {
            ServiceBusConnectionId = serviceBusConnectionId;
        }
    }
}
