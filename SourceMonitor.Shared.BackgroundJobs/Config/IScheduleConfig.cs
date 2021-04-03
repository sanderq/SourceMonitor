using System;
using Cronos;

namespace SourceMonitor.Shared.BackgroundJobs.Config
{
    public interface IScheduleConfig<T>
    {
        CronExpression CronExpression { get; set; }
        TimeZoneInfo TimeZoneInfo { get; set; }
    }
}