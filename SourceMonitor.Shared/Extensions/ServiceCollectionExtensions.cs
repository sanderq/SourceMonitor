using Microsoft.AspNetCore.Builder;
using SourceMonitor.Shared.Middleware;

namespace SourceMonitor.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IApplicationBuilder UseApiKeyMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiKeyMiddleware>();
            return app;
        }
    }
}
