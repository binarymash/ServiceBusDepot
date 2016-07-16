using System;
namespace ServiceBusDepot.ConsoleHost.Features
{
    public class ConsoleColorRegion : IDisposable
    {
        private System.ConsoleColor _parentColor;

        public ConsoleColorRegion(ConsoleColor color)
        {
            _parentColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = color;
        }

        public void Dispose()
        {
            System.Console.ForegroundColor = _parentColor;
        }
    }
}
