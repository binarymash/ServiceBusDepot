using System;
namespace ServiceBusDepot.ConsoleHost.Features
{
    public class ConsoleColorRegion : IDisposable
    {
        private System.ConsoleColor _parentForegroundColor;
        private System.ConsoleColor _parentBackgroundColor;

        public ConsoleColorRegion(ConsoleColor color) : this(color, Console.BackgroundColor)
        {
        }

        public ConsoleColorRegion(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            _parentForegroundColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = foregroundColor;

            _parentBackgroundColor = System.Console.BackgroundColor;
            System.Console.BackgroundColor = backgroundColor;

        }

        public void Dispose()
        {
            System.Console.ForegroundColor = _parentForegroundColor;
            System.Console.BackgroundColor = _parentBackgroundColor;
        }
    }
}
