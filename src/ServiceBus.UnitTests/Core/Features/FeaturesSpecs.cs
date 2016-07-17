using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceBusDepot.Core.Database;

namespace ServiceBusDepot.Testing.UnitTests.Core.Features
{
    public class FeaturesSpecs<T> where T : Profile, new()
    {
        protected ServiceBusDepotContext _database;

        protected MapperConfiguration _configuration;

        protected IMapper _mapper;

        protected void GivenTheInMemoryDatabase()
        {
            //// Bit of a hack to ensure a new instance of in memory database for each test
            //// See https://github.com/aspnet/Home/issues/1541
            //var services = new ServiceCollection();

            //services.AddEntityFrameworkInMemoryDatabase()
            //    .AddDbContext<PackageManagerContext>(x => x
            //        .UseInMemoryDatabase()
            //        .UseInternalServiceProvider(services.BuildServiceProvider()));

            //_db = services.BuildServiceProvider().GetRequiredService<PackageManagerContext>();

            // In future, should hopefully just be able to do this...
            var optionsBuilder = new DbContextOptionsBuilder<ServiceBusDepotContext>();
            optionsBuilder.UseInMemoryDatabase();
            _database = new ServiceBusDepotContext(optionsBuilder.Options);
        }

        protected void GivenTheMappingProfile()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<T>();
            });

            _mapper = _configuration.CreateMapper();
        }
    }
}
