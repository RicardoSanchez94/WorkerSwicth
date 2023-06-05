using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

namespace WorkerCauCapa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        // Host.CreateDefaultBuilder(args)
        //     .ConfigureLogging(options => options.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Information))
        //                 .ConfigureServices((hostContext, services) =>
        //                 {

        //                     services.AddHostedService<Worker>()
        //                     .Configure<EventLogSettings>(config =>
        //                     {


        //                         config.LogName = "LogWokerWorkerCauCapa";
        //                         config.SourceName = "ServiceWorkerCauCapa";
        //                     });
        //                     services.AddHttpClient();
        //                 }).UseWindowsService();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(options => options.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Information))
                        .ConfigureServices((hostContext, services) =>
                        {

                            services.AddHostedService<Worker>()
                            .Configure<EventLogSettings>(config =>
                            {


                                config.LogName = "LogWokerTrxSw";
                                config.SourceName = "ServiceWorkerTrxSw";
                            });
                            services.AddHttpClient();
                        }).UseWindowsService();
    }
}
