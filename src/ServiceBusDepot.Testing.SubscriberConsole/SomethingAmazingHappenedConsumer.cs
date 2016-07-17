namespace ServiceBusDepot.Testing.SubscriberConsole
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using Messages;
    using Newtonsoft.Json;

    public class SomethingAmazingHappenedConsumer : IConsumer<Messages.SomethingAmazingHappened>
    {
        public Task Consume(ConsumeContext<SomethingAmazingHappened> context)
        {
            if (context.Message.Amazingness < 5)
            {
                System.Console.ForegroundColor = System.ConsoleColor.Red;
                System.Console.WriteLine(JsonConvert.SerializeObject(context.Headers));
                System.Console.WriteLine(JsonConvert.SerializeObject(context.Message));
                throw new NotImplementedException();
            }

            System.Console.ForegroundColor = System.ConsoleColor.White;
            System.Console.WriteLine(JsonConvert.SerializeObject(context.Headers));
            System.Console.WriteLine(JsonConvert.SerializeObject(context.Message));
            return Task.FromResult(0);
        }
    }
}
