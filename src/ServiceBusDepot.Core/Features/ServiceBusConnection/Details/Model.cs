using System.Collections.Generic;

namespace ServiceBusDepot.Core.Features.ServiceBusConnection.Details
{
    public class Model
    {
        public int ServiceBusConnectionId { get; private set;}

        public string Description { get; set; }

        public IEnumerable<Queue> Queues { get; private set; }

        public Model(int id, string description, IEnumerable<Queue> queues)
        {
            ServiceBusConnectionId = id;
            Description = description;
            Queues = queues;
        }
    }
}
