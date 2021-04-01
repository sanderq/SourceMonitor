using System.Collections.Generic;

namespace SourceMonitor.WebApplication.Settings
{
    public class WebApplicationSettings
    {
        public IEnumerable<HttpClientSettings> HttpClientSettings = new List<HttpClientSettings>();
    }

    public class HttpClientSettings
    {
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public string Port { get; set; }
    }
}
