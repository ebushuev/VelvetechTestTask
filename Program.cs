using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace TodoApi {
    public class Program {
        public static void Main( string[] args ) {
            CreateHostBuilder ( args ).Build ().Run ();
        }

        public static IHostBuilder CreateHostBuilder( string[] args ) =>
            Host.CreateDefaultBuilder ( args ).ConfigureLogging ( ( hostingContext, logging ) => {
                logging.ClearProviders ();
                logging.AddConfiguration ( hostingContext.Configuration.GetSection ( "Logging" ) );
                logging.AddNLog ();
            } )
            .ConfigureWebHostDefaults ( webBuilder => {
                webBuilder.UseStartup<Startup> ();
            } );
    }
}
