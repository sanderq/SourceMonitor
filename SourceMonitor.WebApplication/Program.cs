using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SourceMonitor.Shared.Constants;
using SourceMonitor.WebApplication.Settings;

namespace SourceMonitor.WebApplication
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient("AzureSourceControlService", c =>
            {
                var baseAddress = builder.Configuration.GetValue<string>("BaseUrl");
                var appsettings = builder.Configuration.GetSection("WebApplicationSettings:HttpClientSettings").Get<List<HttpClientSettings>>();
                var azureSettings = appsettings.Single(x => x.Name.Equals(SourceMonitorConstants.AzureSourceControlServiceName));

                c.BaseAddress = new Uri($"{azureSettings.BaseUrl}:{azureSettings.Port}");
                c.DefaultRequestHeaders.Add(SourceMonitorConstants.ApiKey, "ThisShouldBeSecret");
            });

            await builder.Build().RunAsync();
        }
    }
}
