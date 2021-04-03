using Cronos;

namespace SourceMonitor.Shared.BackgroundJobs.Helper
{
    public static class CronExpressionHelper
    {
        public static readonly CronExpression Yearly = CronExpression.Parse("0 0 1 1 *");
        public static readonly CronExpression Weekly = CronExpression.Parse("0 0 * * 0");
        public static readonly CronExpression Monthly = CronExpression.Parse("0 0 1 * *");
        public static readonly CronExpression Daily = CronExpression.Parse("0 0 * * *");
        public static readonly CronExpression Hourly = CronExpression.Parse("0 * * * *");
        public static readonly CronExpression Minutely = CronExpression.Parse("* * * * *");
        public static readonly CronExpression Secondly = CronExpression.Parse("* * * * * *", CronFormat.IncludeSeconds);
    }
}