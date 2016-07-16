namespace ServiceBusDepot.ConsoleHost.Features.ServiceBusConnections.Details
{
    public class Request : IPageRequest
    {
        public int ServiceBusConnectionId { get; private set; }

        public Request(int serviceBusConnectionId)
        {
            ServiceBusConnectionId = serviceBusConnectionId;
        }
    }
}
