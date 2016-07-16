namespace ServiceBusDepot.ConsoleHost.Features.Queues.Listen
{
    public class PageOption : Features.PageOption
    {
        public PageOption(string key, string queueName) : base(
            key, 
            string.Format("Listen to {0}", queueName),
            new Queues.Listen.Request()             
            )
        {
        }
    }
}
