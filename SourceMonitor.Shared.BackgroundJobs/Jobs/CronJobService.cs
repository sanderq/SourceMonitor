using System;
using System.Threading;
using System.Threading.Tasks;
using Cronos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SourceMonitor.Shared.BackgroundJobs.Jobs
{

    /// <summary>
    /// Credits goes to Changhui Xu
    /// https://codeburst.io/schedule-cron-jobs-using-hostedservice-in-asp-net-core-e17c47ba06
    /// </summary>
    public abstract class CronJobService : IHostedService, IDisposable
    {
        private System.Timers.Timer _timer;
        private readonly ILogger<CronJobService> _logger;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;

        protected CronJobService(ILogger<CronJobService> logger, CronExpression cronExpression, TimeZoneInfo timeZoneInfo)
        {
            _logger = logger;
            _expression = cronExpression;
            _timeZoneInfo = timeZoneInfo;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await ScheduleJob(cancellationToken);
        }

        protected async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                if (delay.TotalMilliseconds <= 0)
                {
                    await ScheduleJob(cancellationToken);
                }
                _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) =>
                {
                    _timer.Dispose();
                    _timer = null;

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        _logger.LogInformation($"Scheduled Job {this.GetType().Name} started at {DateTime.Now.ToLocalTime()}");
                        try
                        {
                            await ExecuteAsync(cancellationToken);

                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e,$"Scheduled Job {GetType().Name} unexpectedly failed at {DateTime.Now.ToLocalTime()}");
                        }
                        _logger.LogInformation($"Scheduled Job {GetType().Name} stopped at {DateTime.Now.ToLocalTime()}");
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await ScheduleJob(cancellationToken);   
                    }
                };
                _timer.Start();
            }
        }

        public abstract Task ExecuteAsync(CancellationToken cancellationToken);
        
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
