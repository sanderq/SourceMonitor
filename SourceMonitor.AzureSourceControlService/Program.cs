using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SourceMonitor.AzureSourceControlService.Settings;
using SourceMonitor.Shared.Extensions;

namespace SourceMonitor.AzureSourceControlService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddAppSettingsJsonsAndEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseStartup<Startup>()
                        .UseKestrel((context, options) =>
                        {
                            var applicationSettings = new AzureApplicationSettings();
                            context.Configuration.GetSection("AzureApplicationSettings").Bind(applicationSettings);
                            options.Listen(IPAddress.Any, applicationSettings.Port, listenOptions => listenOptions.UseHttps());
                        });
                });
    }
}
