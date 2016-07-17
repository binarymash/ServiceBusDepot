using System.Threading.Tasks;
using MediatR;
using ServiceBusDepot.Core.Database;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.ServiceBus.Messaging;
using System.Collections.Generic;

namespace ServiceBusDepot.Core.Features.Queue.Inspect
{
    public class QueryHandler : IAsyncRequestHandler<Query, Model>
    {
        ServiceBusDepotContext _database;

        IMapper _mapper;

        public QueryHandler(ServiceBusDepotContext database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<Model> Handle(Query message)
        {
            var connection = await _database.Connections.FirstAsync(c => c.ServiceBusConnectionId == message.ServiceBusConnectionId);

            var queueClient = QueueClient.CreateFromConnectionString(connection.ConnectionString, message.QueuePath, ReceiveMode.PeekLock);

            var brokeredMessages = await queueClient.PeekBatchAsync(100);

            return new Model(connection.ServiceBusConnectionId, connection.Description, _mapper.Map<IEnumerable<Message>>(brokeredMessages));
        }
    }
}
