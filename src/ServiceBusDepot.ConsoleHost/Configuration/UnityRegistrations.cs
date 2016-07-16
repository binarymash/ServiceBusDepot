namespace ServiceBusDepot.ConsoleHost.Configuration
{
    using Microsoft.Practices.Unity;

    public class UnityRegistrations : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.AddNewExtension<MediatrConfiguration>();
            Container.AddNewExtension<DatabaseConfiguration>();
            Container.AddNewExtension<AutoMapperConfiguration>();

            Container.RegisterType<Application>();
        }
    }
}
