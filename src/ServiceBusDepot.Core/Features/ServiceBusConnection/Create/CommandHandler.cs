using System;
using System.Threading.Tasks;
using MediatR;
using ServiceBusDepot.Core.Database;
using AutoMapper;

namespace ServiceBusDepot.Core.Features.ServiceBusConnection.Create
{
    public class CommandHandler : IAsyncRequestHandler<Command, int>
    {
        ServiceBusDepotContext _database;

        IMapper _mapper;

        public CommandHandler(ServiceBusDepotContext database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task<int> Handle(Command message)
        {
            var connection = _mapper.Map<Entities.ServiceBusConnection>(message);
            _database.Connections.Add(connection);
            return await _database.SaveChangesAsync();
        }
    }
}
