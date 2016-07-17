namespace ServiceBusDepot.ConsoleHost.Features.ServiceBusConnections.Create
{
    using MediatR;

    public class RequestHandler : PageRequestHandler<Request>
    {
        public RequestHandler(IMediator mediator) : base("Create Service Bus Connection", mediator)
        {
        }

        public override IPageRequest Handle(Request message)
        {
            string connectionString;
            string description;

            System.Console.Write("Enter the connection string for the service bus namespace: ");
            using (ConsoleColorManager.InputOption)
            {
                connectionString = System.Console.ReadLine();
            }
            
            System.Console.Write("Enter a description for this connection: ");
            using (ConsoleColorManager.InputOption)
            {
                description = System.Console.ReadLine();
            }


            var command = new Core.Features.ServiceBusConnection.Create.Command(connectionString, description);
            var result = Mediator.SendAsync<int>(command);

            System.Console.WriteLine("Connection string created.");

            return new Features.ServiceBusConnections.Index.Request();
        }
    }
}
