using System;
using Microsoft.Extensions.Configuration;

namespace SourceMonitor.Shared.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddAppSettingsJsonsAndEnvironmentVariables(this IConfigurationBuilder configurationBuilder)
        {
            return configurationBuilder
                .AddJsonFile("appsettings.json", true, false)
                .AddJsonFile($"appsettings.{Environment.MachineName}.json", true, true)
                .AddEnvironmentVariables("SourceMonitor_");
        }
    }
}
