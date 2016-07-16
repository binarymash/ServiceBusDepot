using System;
using MediatR;

namespace ServiceBusDepot.ConsoleHost.Features.Queues.Listen
{
    class RequestHandler : PageRequestHandler<Request>
    {
        public RequestHandler(IMediator mediator) : base("Listen to queue", mediator)
        {
        }

        public override IPageRequest Handle(Request message)
        {
            return GetNextAction(new[]
            {
                new Application.Exit.PageOption("X")
            });
        }
    }
}
