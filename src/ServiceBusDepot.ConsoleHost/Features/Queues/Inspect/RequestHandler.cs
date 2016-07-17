using MediatR;
using Newtonsoft.Json;

namespace ServiceBusDepot.ConsoleHost.Features.Queues.Inspect
{
    public class RequestHandler : PageRequestHandler<Request>
    {
        public RequestHandler(IMediator mediator) : base(string.Format("Messages on queue"), mediator)
        {
        }

        public override IPageRequest Handle(Request message)
        {
            var inspectQuery = new Core.Features.Queue.Inspect.Query(message.ServiceBusConnectionId, message.QueuePath);
            var messages = Mediator.SendAsync(inspectQuery).ConfigureAwait(false).GetAwaiter().GetResult();

            int row = 0;
            foreach(var queueMessage in messages.Messages)
            {
                using(++row % 2 == 0 ? ConsoleColorManager.EvenDataRow : ConsoleColorManager.OddDataRow)
                {
                    System.Console.WriteLine(System.Text.RegularExpressions.Regex.Unescape(JsonConvert.SerializeObject(queueMessage, Formatting.Indented)));
                    System.Console.WriteLine();
                }

                //System.Console.Write("MessageId: ");
                //using (ConsoleColorManager.Data)
                //{
                //    System.Console.WriteLine(queueMessage.MessageId);
                //}
                //System.Console.Write("Body: ");
                //using (ConsoleColorManager.Data)
                //{
                //    System.Console.WriteLine(queueMessage.Body);
                //}
            }

            return GetNextAction(new ServiceBusConnections.Details.PageOption("R", message.ServiceBusConnectionId));
        }
    }
}
