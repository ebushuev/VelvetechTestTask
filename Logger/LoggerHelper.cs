namespace TodoApiDTO.Logger
{
    using System;
    using NLog;

    /// <summary>
    /// Хелпер для логгера NLog.
    /// </summary>
    public static class LoggerHelper
    {
        /// <summary>
        /// Логгирование исключения в консоль
        /// и файл корневой директории.
        /// </summary>
        /// <param name="isCustomError"></param>
        /// <param name="exception"></param>
        public static void LogError(bool isCustomError, Exception exception)
        {
            if (isCustomError)
            {
                LogCustomError(exception);
            }
            else
            {
                LogSystemError(exception);
            }
        }

        /// <summary>
        /// Логгирование системного исключения в консоль
        /// и файл корневой директории /logs с соответствующим префиксом.
        /// </summary>
        /// <param name="exception">Исключение.</param>
        private static void LogSystemError(Exception exception)
        {
            LogManager
                .GetLogger("SystemError")
                .Error(exception
                    + Environment.NewLine
                    + Environment.NewLine);
        }

        /// <summary>
        /// Логгирование кастомного исключения в консоль
        /// и файл корневой директории /logs с соответствующим префиксом.
        /// </summary>
        /// <param name="exception">Исключение.</param>
        private static void LogCustomError(Exception exception)
        {
            LogManager
                .GetLogger("CustomError")
                .Error(exception
                       + Environment.NewLine
                       + Environment.NewLine);
        }
    }
}