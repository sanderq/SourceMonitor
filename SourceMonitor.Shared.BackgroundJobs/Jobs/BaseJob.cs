using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Server;

namespace SourceMonitor.Shared.BackgroundJobs.Jobs
{
    public abstract class BaseJob
    {
        protected BaseJob()
        {
            Id = GetType().Name;
        }

        protected string Id { get; set; }
        public abstract string Queue { get; }
        public virtual void CreateRecurringJob(bool startImmediately = false)
        {
            RecurringJob.AddOrUpdate(Id, () => ExecuteAsync(null), Cron.Minutely, queue: Queue);
            if (startImmediately)
            {
                TriggerJob();
            }
        }

        protected void TriggerJob()
        {
            var monitoring = JobStorage.Current.GetMonitoringApi();
            var jobs = monitoring.ProcessingJobs(0, 50).Select(x => x.Value).ToList();
            if (!jobs.Any(pj => pj.Job != null && pj.Job.Type.Name == Id))
            {
                RecurringJob.Trigger(Id);
            }
        }

        public abstract Task ExecuteAsync(PerformContext context);
    }
}
