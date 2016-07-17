using MediatR;

namespace ServiceBusDepot.Core.Features.Queue.Inspect
{
    public class Query : IAsyncRequest<Model>
    {
        public int ServiceBusConnectionId { get; private set; }

        public string QueuePath { get; private set; }

        public Query(int serviceBusConnectionId, string queuePath)
        {
            ServiceBusConnectionId = serviceBusConnectionId;
            QueuePath = queuePath;
        }
    }
}
