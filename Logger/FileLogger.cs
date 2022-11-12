using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace TodoApiDTO.Logger
{
    [ProviderAlias("FileLogger")]
    public class FileLogger : ILogger
    {
        protected readonly FileLoggerProvider _loggerProvider;

        public FileLogger(FileLoggerProvider loggerProvider)
        {
            _loggerProvider = loggerProvider;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            string fullFilePath = string.Format(@$"{_loggerProvider.Options.FolderPath}/{_loggerProvider.Options.FilePath}");

            var logRecord = string.Format(@$"{DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")} {logLevel} {formatter(state, exception)} {(exception != null ? exception.StackTrace : "")}");

            using (var streamWritter = new StreamWriter(fullFilePath, true))
            {
                streamWritter.WriteLine(logRecord);
            }
        }
    }
}
