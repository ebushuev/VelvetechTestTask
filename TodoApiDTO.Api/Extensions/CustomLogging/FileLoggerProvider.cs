using System;
using Microsoft.Extensions.Logging;

namespace TodoApiDTO.Api.Extensions.CustomLogging
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _path;

        public FileLoggerProvider(string path)
        {
            _path = !string.IsNullOrWhiteSpace(path)
                ? path
                : throw new ArgumentNullException(nameof(path));
        }

        #region ILoggerProvider Members

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_path);
        }

        public void Dispose()
        {
        }

        #endregion
    }
}