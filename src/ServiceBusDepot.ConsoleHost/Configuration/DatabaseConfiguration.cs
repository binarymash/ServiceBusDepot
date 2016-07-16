namespace ServiceBusDepot.ConsoleHost.Configuration
{
    using Core.Database;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Practices.Unity;

    public class DatabaseConfiguration : UnityContainerExtension
    {        
        protected override void Initialize()
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ServiceBusDepotContext>();
            dbContextOptionsBuilder.UseSqlite("Filename=./servicebusdepot.db");
            Container.RegisterInstance(dbContextOptionsBuilder.Options);
            Container.RegisterType<ServiceBusDepotContext>(ContextFactory());
        }

        private static InjectionFactory ContextFactory()
        {
            return new InjectionFactory(c => new ServiceBusDepotContext(c.Resolve<DbContextOptions<ServiceBusDepotContext>>()));
        }
    }
}
