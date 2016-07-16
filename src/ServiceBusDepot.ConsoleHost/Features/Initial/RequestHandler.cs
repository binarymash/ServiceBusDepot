namespace ServiceBusDepot.ConsoleHost.Features.Initial
{
    using System;
    using MediatR;

    public class QueryHandler: PageRequestHandler<Request>  
    {
        public QueryHandler(IMediator mediator) : base("Initialising....", mediator)
        {
        }

        public override IPageRequest Handle(Request message)
        {
            System.Console.WriteLine("Checking for service bus connection strings...");
            var connections = Mediator
                .SendAsync(new Core.Features.ServiceBusConnection.Index.Query())
                .ConfigureAwait(false).GetAwaiter().GetResult();

            if (connections.Count == 0)
            {
                System.Console.WriteLine("There are no service bus connections.");
                return new Features.ServiceBusConnections.Create.Request();

            }

            return new Features.Main.Request();
        }
    }
}
