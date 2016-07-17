using MediatR;

namespace ServiceBusDepot.Core.Features.ServiceBusConnection.Create
{
    public class Command : IAsyncRequest<int>
    {
        public string ConnectionString { get; private set; }

        public string Description{get;private set;}

        public Command(string connectionString, string description)
        {
            ConnectionString = connectionString;
            Description = description;
        }
    }
}
