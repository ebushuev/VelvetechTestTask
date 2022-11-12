using Serilog.Events;
using Serilog;

namespace TodoApiDTO.Logger
{
    public static class LoggerConfigure
    {
        public static void AddSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
                .WriteTo.File("LogTodoWebApi-.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .CreateLogger();
        }
    }
}
