﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.ServiceBus;
using ServiceBusDepot.Core.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceBusDepot.Core.Features.ServiceBusConnection.Details
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

            var namespaceManager = NamespaceManager.CreateFromConnectionString(connection.ConnectionString);

            var queueDescriptions = await namespaceManager.GetQueuesAsync();

            return new Model(connection.ServiceBusConnectionId, connection.Description, _mapper.Map<IEnumerable<Queue>>(queueDescriptions));
        }
    }
}
