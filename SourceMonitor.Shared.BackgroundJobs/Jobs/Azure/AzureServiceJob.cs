using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SourceMonitor.Shared.BackgroundJobs.Config;

namespace SourceMonitor.Shared.BackgroundJobs.Jobs.Azure
{
    public class AzureServiceJob : CronJobService
    {
        public AzureServiceJob(IScheduleConfig<AzureServiceJob> config, ILogger<AzureServiceJob> logger) : base(logger, config.CronExpression, config.TimeZoneInfo)
        {
        }

        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"Fired at {DateTime.Now.ToUniversalTime()}");
        }
    }
}
