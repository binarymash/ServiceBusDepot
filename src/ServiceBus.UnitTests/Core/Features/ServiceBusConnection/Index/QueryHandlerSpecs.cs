using System.Collections.Generic;
using ServiceBusDepot.Core.Features.ServiceBusConnection.Index;
using Shouldly;
using TestStack.BDDfy;
using Xunit;

namespace ServiceBusDepot.Testing.UnitTests.Core.Features.ServiceBusConnection.Index
{
    using Entities = ServiceBusDepot.Core.Entities;

    public class QueryHandlerSpecs : ServiceBusConnectionSpecs
    {

        QueryHandler _handler;

        List<Entities.ServiceBusConnection> _connectionsInDatabase;

        Query _query;

        List<Model> _results;

        public QueryHandlerSpecs()
        {
            GivenTheInMemoryDatabase();
            GivenTheMappingProfile();

            _handler = new QueryHandler(_database, _mapper);
        }

        [Fact]
        public void ShouldHandleIndexQueryWhenThereAreNoConnections()
        {
            this.Given(_ => _.GivenThereAreNoServiceBusConnectionsInTheDatabase())
                .When(_ => _.WhenAnIndexQueryIsHandled())
                .Then(_ => _.TheAnEmptyCollectionOfConnectionsIsReturned())
                .BDDfy();
        }

        [Fact]
        public void ShouldAllowQueryingOfCollections()
        {
            this.Given(_ => _.GivenThereAreServiceBusConnectionsInTheDatabase())
                .When(_ => _.WhenAnIndexQueryIsHandled())
                .Then(_ => _.ThenTheConnectionsAreReturned())
                .BDDfy();
        }

        private void GivenThereAreNoServiceBusConnectionsInTheDatabase()
        {
        }

        private void GivenThereAreServiceBusConnectionsInTheDatabase()
        {
            _connectionsInDatabase = new List<Entities.ServiceBusConnection>()
            {
                new Entities.ServiceBusConnection()
                {
                    ServiceBusConnectionId = 1,
                    ConnectionString = "A connection string",
                    Description = "A description"
                },
                new Entities.ServiceBusConnection()
                {
                    ServiceBusConnectionId = 2,
                    ConnectionString = "Another connection string",
                    Description = "Another description"
                }
            };

            _database.Connections.AddRange(_connectionsInDatabase);
            _database.SaveChanges();
        }

        private void WhenAnIndexQueryIsHandled()
        {
            _query = new Query();
            _results = _handler.Handle(_query).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private void TheAnEmptyCollectionOfConnectionsIsReturned()
        {
            _results.Count.ShouldBe(0);
        }


        private void ThenTheConnectionsAreReturned()
        {
            _results.Count.ShouldBe(_connectionsInDatabase.Count);
            foreach (var connectionInDatabase in _connectionsInDatabase)
            {
                _results.Exists(returnedConnection => Matching(returnedConnection, connectionInDatabase)).ShouldBeTrue();
            };
        }

        private bool Matching(Model c, Entities.ServiceBusConnection connection)
        {
            return 
                c.ServiceBusConnectionId == connection.ServiceBusConnectionId && 
                c.Description == connection.Description;
        }
    }
}
