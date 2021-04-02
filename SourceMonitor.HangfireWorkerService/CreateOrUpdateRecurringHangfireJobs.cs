using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SourceMonitor.Shared.BackgroundJobs.Jobs;

namespace SourceMonitor.HangfireWorkerService
{
    public class CreateOrUpdateRecurringHangfireJobs : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public CreateOrUpdateRecurringHangfireJobs(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var jobs = _serviceProvider.GetServices<BaseJob>();
            foreach (var job in jobs)
            {
                job.CreateRecurringJob(true);
            }
        }
    }
}