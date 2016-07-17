namespace ServiceBusDepot.ConsoleHost.Features.Queues.Inspect
{
    public class PageOption : Features.PageOption
    {
        public PageOption(string key, int serviceBusConnectionId, string queuePath) : base(
            key, 
            string.Format("Inspect the messages on {0}", queuePath),
            new Queues.Inspect.Request(serviceBusConnectionId, queuePath)             
            )
        {
        }
    }
}
