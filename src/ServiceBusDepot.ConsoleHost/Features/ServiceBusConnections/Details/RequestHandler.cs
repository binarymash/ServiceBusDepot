using System;
using MediatR;
using System.Collections.Generic;

namespace ServiceBusDepot.ConsoleHost.Features.ServiceBusConnections.Details
{
    public class RequestHandler : PageRequestHandler<Request>
    {
        public RequestHandler(IMediator mediator) : base("Service Bus Connection Details", mediator)
        {
        }

        public override IPageRequest Handle(Request message)
        {
            var request = new Core.Features.ServiceBusConnection.Details.Query(message.ServiceBusConnectionId);
            var connectionDetails = Mediator.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();

            var queueDictionary = new Dictionary<int, Core.Features.ServiceBusConnection.Details.Queue>();

            int queueKey = 0;
            foreach (var queue in connectionDetails.Queues)
            {
                queueDictionary.Add(++queueKey, queue);
            }

            System.Console.WriteLine("Queues:");
            System.Console.WriteLine();

            foreach (var key in queueDictionary.Keys)
            {
                System.Console.Write("Path: ");
                using (ConsoleColorManager.Data)
                {
                    System.Console.Write(queueDictionary[key].Path);
                }
                System.Console.Write(" (");
                using (ConsoleColorManager.Data)
                {
                    System.Console.Write(queueDictionary[key].MessageCount);
                }
                System.Console.WriteLine(" messages)");
            }

            var pageOptions = new List<Features.PageOption>();
            foreach(var key in queueDictionary.Keys)
            {
                pageOptions.Add(new Features.Queues.Inspect.PageOption(key.ToString(), message.ServiceBusConnectionId, queueDictionary[key].Path));
            }
            pageOptions.Add(new Application.Exit.PageOption("X"));

            return GetNextAction(pageOptions);
        }
    }
}
