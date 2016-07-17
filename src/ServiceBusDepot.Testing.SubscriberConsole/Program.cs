using MassTransit;
using MassTransit.AzureServiceBusTransport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusDepot.Testing.SubscriberConsole
{
    class Program
    {
        private IBusControl _busControl;

        static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }

        public void Run()
        {
            System.Console.WriteLine("Initializing bus...");
            System.Console.WriteLine();

            if (!InitializeBus())
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Press any key to exit");
                System.Console.ReadKey();
                return;
            }

            var exit = false;
            while (!exit)
            {
                System.Console.WriteLine("1. Exit");
                var input = System.Console.ReadLine();
                switch (input)
                {
                    case "1":
                        exit = true;
                        break;
                }
            }
        }

        private bool InitializeBus()
        {
            const string connectionStringKey = "ServiceBusDepot.Testing.Connection";

            var connectionString = System.Configuration.ConfigurationManager.AppSettings[connectionStringKey];
            if (String.IsNullOrEmpty(connectionString))
            {
                connectionString = System.Environment.GetEnvironmentVariable(connectionStringKey);
            }

            if (string.IsNullOrEmpty(connectionString))
            {
                System.Console.ForegroundColor = System.ConsoleColor.Red;
                System.Console.WriteLine("No connection string was found. Either add an appsetting key, or an environment variable, with the name {0}", connectionStringKey);
                return false;
            }

            _busControl = Bus.Factory.CreateUsingAzureServiceBus(configuration =>
            {
                var host = configuration.Host(
                    connectionString,
                    cfg =>
                    {
                    });

                configuration.ReceiveEndpoint(
                    host,
                    "ServiceBusDepot.Testing.SubscriberConsole-SomethingAmazingHappened",
                    cfg =>
                    {
                        cfg.MaxConcurrentCalls = 1;
                        cfg.PrefetchCount = 5;
                        cfg.Consumer(() => new SomethingAmazingHappenedConsumer());
                    });
            });

            _busControl.Start();

            return true;
        }
    }
}
