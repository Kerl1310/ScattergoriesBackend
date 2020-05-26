using static ScattergoriesBackend.Helpers.ApiLogger;

namespace ScattergoriesBackend.Interfaces
{
    public interface ILogger
    {
            void Log(string content, LogSeverity severity);
    }
}
