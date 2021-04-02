using System;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Console;
using Hangfire.Heartbeat;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SourceMonitor.Shared.BackgroundJobs.Jobs;
using SourceMonitor.Shared.BackgroundJobs.Jobs.Azure;
using SourceMonitor.Shared.Constants;

namespace SourceMonitor.HangfireWorkerService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);
            await hostBuilder.Build().RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(async (hostContext, services) =>
                {
                    services.AddSingleton<BaseJob, AzureServiceJob>();
                    services.AddHangfireServer(config =>
                    {
                        config.ServerName = "SourceMonitor.HangfireWorkerService";
                        config.Queues = new[] { SourceMonitorConstants.AzureSourceControlServiceName.ToLowerInvariant() };
                    });
                    services.AddHangfire(config =>
                    {
                        config.UseMemoryStorage();
                        config.UseHeartbeatPage(checkInterval: TimeSpan.FromSeconds(1));
                        config.UseConsole();
                    });
                    services.AddHostedService<CreateOrUpdateRecurringHangfireJobs>();
                });
    }
}
