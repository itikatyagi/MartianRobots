using System;

namespace MartianRobots.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine($"[RobotLog] {message}");
    }
}