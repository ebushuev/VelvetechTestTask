using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApiDTO.Business.Repositories.Interface;

namespace TodoApiDTO.Business.Repositories.Services
{
    public class LoggerService: ILoggerService
    {
        private static ILogger _logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();//LogManager.GetCurrentClassLogger();
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }
    }
}
