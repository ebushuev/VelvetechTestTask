using System;
using System.IO;
using Microsoft.Extensions.Logging;
using TodoApiDTO.Api.Helpers;

namespace TodoApiDTO.Api.Extensions.CustomLogging
{
    public class FileLogger : ILogger, IDisposable
    {
        #region Static

        private static readonly object Lock = new object();

        #endregion

        private readonly string filePath;

        public FileLogger(string path)
        {
            filePath = path;
        }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        #region ILogger Members

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId,
            TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            lock (Lock)
            {
                ApplicationHelpers.PrepareCatalogOfFile(filePath);
                File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
            }
        }

        #endregion
    }
}