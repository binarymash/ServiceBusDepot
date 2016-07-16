namespace ServiceBusDepot.ConsoleHost
{
    using Features;
    using MediatR;

    public class Application
    {
        IMediator _mediator;

        public Application(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void Run(IPageRequest nextCommand)
        {
            while(nextCommand != null)
            {
                nextCommand = _mediator.Send(nextCommand);
            }
        }
    }
}
