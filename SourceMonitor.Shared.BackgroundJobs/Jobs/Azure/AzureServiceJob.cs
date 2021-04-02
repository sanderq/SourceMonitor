using System.Threading.Tasks;
using Hangfire.Console;
using Hangfire.Server;
using SourceMonitor.Shared.Constants;

namespace SourceMonitor.Shared.BackgroundJobs.Jobs.Azure
{
    public class AzureServiceJob : BaseJob
    {
        public override string Queue => SourceMonitorConstants.AzureSourceControlServiceName.ToLowerInvariant();

        public override async Task ExecuteAsync(PerformContext context)
        {
            context.WriteLine("Error!");
        }
    }
}
