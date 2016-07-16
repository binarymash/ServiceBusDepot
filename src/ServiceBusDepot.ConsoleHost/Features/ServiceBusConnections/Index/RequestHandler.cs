using MediatR;
using System.Collections.Generic;

namespace ServiceBusDepot.ConsoleHost.Features.ServiceBusConnections.Index
{
    public class RequestHandler : PageRequestHandler<Request>
    {
        public RequestHandler(IMediator mediator) : base("Service Bus Connections", mediator)
        {
        }

        public override IPageRequest Handle(Request message)
        {
            var query = new Core.Features.ServiceBusConnection.Index.Query();

            var connections = Mediator.SendAsync(query)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            foreach (var connection in connections)
            {
                System.Console.Write("Id: ");
                using (ConsoleColorManager.Data)
                {
                    System.Console.Write(connection.ServiceBusConnectionId);
                }
                System.Console.Write("; Description: ");
                using (ConsoleColorManager.Data)
                {
                    System.Console.WriteLine(connection.Description);
                }
            }

            var pageOptions = new List<Features.PageOption>()
            {
                new ServiceBusConnections.Create.PageOption("A")
            };

            foreach(var connection in connections)
            {
                pageOptions.Add(new ServiceBusConnections.Details.PageOption(connection.ServiceBusConnectionId.ToString(), connection.ServiceBusConnectionId, connection.Description));
            };

            pageOptions.Add(new Application.Exit.PageOption("X"));

            return GetNextAction(pageOptions);
        }
    }
}
