using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SourceMonitor.BackgroundJobWorkerService.Extensions;
using SourceMonitor.Shared.BackgroundJobs.Helper;
using SourceMonitor.Shared.BackgroundJobs.Jobs.Azure;

namespace SourceMonitor.BackgroundJobWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddCronJob<AzureServiceJob>(config =>
                    {
                        config.TimeZoneInfo = TimeZoneInfo.Utc;
                        config.CronExpression = CronExpressionHelper.Secondly;
                    });
                });

    }
}
