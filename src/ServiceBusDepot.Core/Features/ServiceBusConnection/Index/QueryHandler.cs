namespace ServiceBusDepot.Core.Features.ServiceBusConnection.Index
{
    using AutoMapper;
    using Database;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class QueryHandler : IAsyncRequestHandler<Query, List<Model>>
    {
        ServiceBusDepotContext _database;

        IMapper _mapper;

        public QueryHandler(ServiceBusDepotContext database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<List<Model>> Handle(Query message)
        {
            var connections = await _database.Connections.ToListAsync();
            return await Task.FromResult(_mapper.Map<List<Model>>(connections));
        }
    }
}
