using System;
using Amazon.Lambda.Core;
using ScattergoriesBackend.Interfaces;

namespace ScattergoriesBackend.Helpers
{
    public class ApiLogger : ILogger
    {
        public static string RequestId { get; set; }

        public void Log(string content, LogSeverity severity)
        {
            Logger(content, severity);
        }

        public static void Logger(string content, LogSeverity severity)
        {
            LambdaLogger.Log($"[{severity}] {DateTime.UtcNow:o} {RequestId} ScattergoriesBackend: {content}");
        }

        public enum LogSeverity
        {
            INFO,
            WARNING,
            ERROR,
        }
    }
}
