using System;
using MediatR;

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

            return GetNextAction(new Application.Exit.PageOption("X"));
        }
    }
}
