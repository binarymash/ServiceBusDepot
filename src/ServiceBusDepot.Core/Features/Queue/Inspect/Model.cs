using System.Collections.Generic;

namespace ServiceBusDepot.Core.Features.Queue.Inspect
{
    public class Model
    {
        public int ServiceBusConnectionId { get; private set; }

        public string QueuePath { get; private set; }

        public IEnumerable<Message> Messages { get; private set; }

        public Model(int serviceBusConnectionId, string queuePath, IEnumerable<Message> messages)
        {
            ServiceBusConnectionId = serviceBusConnectionId;
            QueuePath = queuePath;
            Messages = messages;
        }
    }
}
