namespace ServiceBusDepot.ConsoleHost.Features.Queues.Inspect
{
    public class Request : IPageRequest
    {
        public int ServiceBusConnectionId { get; private set; }

        public string QueuePath { get; private set; }

        public Request(int serviceBusConnectionId, string queuePath)
        {
            ServiceBusConnectionId = serviceBusConnectionId;
            QueuePath = queuePath;
        }
    }
}
