using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace TodoApiDTO.Logging
{
    
    public class FileLogger: ILogger
    {
        private readonly FileLoggerProvider _loggerProvider;

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

            var foo = DateTime.UtcNow.ToString("dd-MM-yyy");

            var path = string.Format("{0}/{1}",
                _loggerProvider.Options.FolderPath,
                _loggerProvider.Options.FilePath.Replace("{date}", DateTime.UtcNow.ToString("ddMMyyyy")));
            var logRecord = string.Format("{0} [{1}] {2} {3}",
                DateTime.UtcNow.ToString("dd-MM-yyy HH:mm:ss"),
                logLevel.ToString(),
                formatter(state, exception),
                (exception != null ? exception.StackTrace : "") );

            using (var streamWriter = new StreamWriter(path, true))
            {
               streamWriter.WriteLine(logRecord); 
            }

        }
    }
}
