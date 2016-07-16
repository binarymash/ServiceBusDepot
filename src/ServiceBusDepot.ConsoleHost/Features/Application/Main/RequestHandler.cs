namespace ServiceBusDepot.ConsoleHost.Features.Application.Main
{
    using MediatR;
    using System.Collections.Generic;

    public class RequestHandler : PageRequestHandler<Request>
    {
        public RequestHandler(IMediator mediator) : base("Main Menu", mediator)
        {
        }

        public override IPageRequest Handle(Request message)
        {
            var pageOptions = new List<PageOption>
            {
                new ServiceBusConnections.Index.PageOption("A"),
                new Exit.PageOption("X")
            };

            return GetNextAction(pageOptions);
        }
    }
}
