namespace ServiceBusDepot.ConsoleHost.Features
{
    public class PageOption
    {
        public string Key { get; private set; }

        public string Description { get; private set; }

        public IPageRequest Action { get; private set; }

        public PageOption(string key, string description, IPageRequest action)
        {
            Key = key;
            Description = description;
            Action = action;
        }
    }
}
