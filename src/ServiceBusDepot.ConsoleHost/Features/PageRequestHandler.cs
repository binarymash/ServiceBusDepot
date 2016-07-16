using MediatR;
using System.Collections.Generic;

namespace ServiceBusDepot.ConsoleHost.Features
{
    public abstract class PageRequestHandler<T> : IRequestHandler<T, IPageRequest> where T: IPageRequest
    {
        protected IMediator Mediator { get; private set; }

        private readonly Dictionary<string, PageOption> PageOptions = new Dictionary<string, PageOption>();

        public PageRequestHandler(string title, IMediator mediator)
        {
            Mediator = mediator;

            System.Console.Clear();

            WriteTitle(title);
        }

        public abstract IPageRequest Handle(T message);

        private void WriteTitle(string title)
        {
            using (ConsoleColorManager.Title)
            {
                System.Console.WriteLine(title);
            }
            System.Console.WriteLine();
        }

        private void SetPageOptions(IList<PageOption> pageOptions)
        {
            foreach (var pageOption in pageOptions)
            {
                var key = pageOption.Key.ToLowerInvariant();
                PageOptions.Add(key, pageOption);
            }
        }

        protected IPageRequest GetNextAction(PageOption pageOption)
        {
            return GetNextAction(new[] { pageOption });
        }

        protected IPageRequest GetNextAction(IList<PageOption> pageOptions)
        {
            SetPageOptions(pageOptions);
            using (ConsoleColorManager.Header)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Make a selection:");
                System.Console.WriteLine();
            }

            ShowPageOptions();

            while (true)
            {
                System.Console.Write("> ");
                using (ConsoleColorManager.InputOption)
                {
                    var selection = System.Console.ReadLine().ToLowerInvariant();

                    if (PageOptions.ContainsKey(selection))
                    {
                        return PageOptions[selection].Action;
                    }
                }

                using (ConsoleColorManager.ErrorMessage)
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine("Invalid option. Please select again.");
                    System.Console.WriteLine();
                }
            }
        }

        protected void ShowPageOptions()
        {
            foreach(var pageOption in PageOptions.Values)
            {
                ShowPageOption(pageOption);
            }
            System.Console.WriteLine();
        }

        protected void ShowPageOption(PageOption pageOption)
        {
            using (ConsoleColorManager.InputOption)
            {
                System.Console.Write(pageOption.Key);
            }

            System.Console.WriteLine(string.Format(" - {1}", pageOption.Key, pageOption.Description));
        }
    }
}
