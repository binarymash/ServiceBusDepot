using MediatR;

namespace ServiceBusDepot.ConsoleHost.Features.Application.Exit
{
    public class RequestHandler : PageRequestHandler<Request>
    {
        public RequestHandler(IMediator mediator) : base("Exiting...", mediator)
        {
        }

        public override IPageRequest Handle(Request message)
        {
            return null;
        }
    }
}
