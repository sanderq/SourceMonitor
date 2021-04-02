using System.Collections.Generic;

namespace SourceMonitor.WebApplication.Settings
{
    public class WebApplicationSettings
    {
        public List<HttpClientSettings> HttpClientSettings { get; set; } = new();
    }
}
