namespace ServiceBusDepot.ConsoleHost.Features.ServiceBusConnections.Create
{
    public class PageOption : Features.PageOption
    {
        public PageOption(string key) : base(key, "Add a new Service Bus Connection", new Request())
        {
        }
    }
}
