using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace TodoApiDTO.Logger
{
    public class FileLogger : ILogger
    {
        private string _path;
        private static object _lock = new object();
        
        public FileLogger(string path)
        {
            _path = path;
        }
        
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LogLevel.Error;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel) && formatter != null)
            {
                lock (_lock)
                {
                    File.AppendAllText(_path, formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}
