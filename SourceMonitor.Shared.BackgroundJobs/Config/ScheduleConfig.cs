using System;
using Cronos;

namespace SourceMonitor.Shared.BackgroundJobs.Config
{
    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        public CronExpression CronExpression { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
    }
}
