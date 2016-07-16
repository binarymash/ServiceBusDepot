using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceBusDepot.Core.Entities
{
    public class ServiceBusConnection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceBusConnectionId { get; set; }

        public string Uri { get; set; }

        public string ConnectionString { get; set; }

        public string Description { get; set; }
    }
}
