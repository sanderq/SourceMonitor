using System;
using System.Threading;
using System.Threading.Tasks;
using SourceMonitor.Shared.BackgroundJobs.Config;

namespace SourceMonitor.Shared.BackgroundJobs.Jobs.Azure
{
    public class AzureServiceJob : CronJobService
    {
        public AzureServiceJob(IScheduleConfig<AzureServiceJob> config) : base(config.CronExpression, config.TimeZoneInfo)
        {
        }

        public override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"Fired at {DateTime.Now.ToUniversalTime()}");
        }
    }
}
