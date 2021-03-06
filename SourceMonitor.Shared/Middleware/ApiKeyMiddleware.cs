using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SourceMonitor.Shared.Constants;

namespace SourceMonitor.Shared.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly IConfiguration _configuration;
        private readonly RequestDelegate _next;
        public ApiKeyMiddleware(IConfiguration configuration ,RequestDelegate next)
        {
            _configuration = configuration;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(SourceMonitorConstants.ApiKey, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var apiKey = _configuration.GetValue<string>(SourceMonitorConstants.ApiKey);

            if (!apiKey.Equals(extractedApiKey, StringComparison.Ordinal))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);
        }
    }
}
