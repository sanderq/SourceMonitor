using Microsoft.Extensions.Hosting;
using System;
using SourceMonitor.CronJobWorkerService.Extensions;
using SourceMonitor.Shared.BackgroundJobs.Helper;
using SourceMonitor.Shared.BackgroundJobs.Jobs.Azure;

namespace SourceMonitor.CronJobWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
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
