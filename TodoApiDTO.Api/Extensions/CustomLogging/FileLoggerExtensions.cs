using Microsoft.Extensions.Logging;

namespace TodoApiDTO.Api.Extensions.CustomLogging
{
    public static class FileLoggerExtensions
    {
        #region Static

        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string filePath)
        {
            builder.AddProvider(new FileLoggerProvider(filePath));
            return builder;
        }

        #endregion
    }
}