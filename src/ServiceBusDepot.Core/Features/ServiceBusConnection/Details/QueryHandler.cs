using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.ServiceBus;
using ServiceBusDepot.Core.Database;
using System.Threading.Tasks;

namespace ServiceBusDepot.Core.Features.ServiceBusConnection.Details
{
    public class QueryHandler : IAsyncRequestHandler<Query, Model>
    {
        ServiceBusDepotContext _database;

        public QueryHandler(ServiceBusDepotContext database)
        {
            _database = database;
        }

        public async Task<Model> Handle(Query message)
        {
            var connection = await _database.Connections.FirstAsync(c => c.ServiceBusConnectionId == message.ServiceBusConnectionId);

            var sas = connection.ConnectionString;

            var tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", sas);

            //var namespaceManager = new NamespaceManager(connection.Uri, tokenProvider);

            //var queueDescriptions = await namespaceManager.GetQueuesAsync();

            //foreach(var queueDescription in queueDescriptions)
            //{
            //}

            return new Model();
        }
    }
}
