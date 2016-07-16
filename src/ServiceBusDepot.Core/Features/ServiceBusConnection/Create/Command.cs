using MediatR;

namespace ServiceBusDepot.Core.Features.ServiceBusConnection.Create
{
    public class Command : IAsyncRequest<int>
    {
        public string Uri { get; private set; }

        public string ConnectionString { get; private set; }

        public string Description{get;private set;}

        public Command(string uri, string connectionString, string description)
        {
            Uri = uri;
            ConnectionString = connectionString;
            Description = description;
        }
    }
}
