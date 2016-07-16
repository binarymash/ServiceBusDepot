namespace ServiceBusDepot.Core.Database
{
    using Microsoft.EntityFrameworkCore;

    public class ServiceBusDepotContext : DbContext
    {
        public ServiceBusDepotContext()
        {
        }

        public ServiceBusDepotContext(DbContextOptions<ServiceBusDepotContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=./servicebusdepot.db");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Entities.ServiceBusConnection> Connections { get; set; }
    }
}
