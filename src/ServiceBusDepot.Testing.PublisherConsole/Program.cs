namespace ServiceBusDepot.Testing.PublisherConsole
{
    using System;
    using MassTransit;
    using MassTransit.AzureServiceBusTransport;

    public class Program
    {
        private IBusControl _busControl;

        public static void Main(string[] args)
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

            System.Console.WriteLine("Bus initialized!");
            System.Console.WriteLine();

            var exit = false;
            while (!exit)
            {
                System.Console.WriteLine("1. SomethingAmazingHappened message");
                System.Console.WriteLine("2. Exit");
                var input = System.Console.ReadLine();
                switch (input)
                {
                    case "1":
                        SendCustomerOrderCreated();

                        System.Console.WriteLine("Message sent.");
                        System.Console.WriteLine();
                        System.Console.WriteLine();
                        break;
                    case "2":
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
                configuration.Host(
                    connectionString,
                    cfg =>
                    {
                    });
            });

            _busControl.Start();

            return true;
        }

        private void SendCustomerOrderCreated()
        {
            System.Console.Write("Something amazing happened. ");

            System.Console.Write("Describe how amazing it was: ");
            var description = System.Console.ReadLine();

            System.Console.Write("Give it a score out of 10: ");
            var amazingness = Convert.ToInt32(System.Console.ReadLine());

            var message = new Messages.SomethingAmazingHappened()
            {
                DescriptionOfHowAmazingItWas = description,
                Amazingness = amazingness
            };

            _busControl.Publish(message).ConfigureAwait(false).GetAwaiter().GetResult();
        }

    }
}
