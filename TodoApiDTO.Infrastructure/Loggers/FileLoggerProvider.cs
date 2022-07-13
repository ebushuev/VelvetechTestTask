using Microsoft.Extensions.Logging;

namespace TodoApiDTO.TodoApiDTO.Infrastructure.Loggers
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string logPath;
        public FileLoggerProvider(string _logpath)
        {
            logPath = _logpath;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(logPath);
        }

        public void Dispose()
        {

        }
    }
}
