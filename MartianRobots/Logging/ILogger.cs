namespace MartianRobots.Logging
{
    /// <summary>Minimal logger abstraction for observability and test injection.</summary>
    public interface ILogger
    {
        void Log(string message);
    }
}