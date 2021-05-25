using Correlate.AspNetCore;
using OEMS.Logger.Middleware;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace OEMS.Logger.Extensions.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseLogCorrelation(this IApplicationBuilder builder)
        {
            builder.UseCorrelate();
            builder.UseMiddleware<LogCorrelationMiddleware>();
            builder.UseSerilogRequestLogging();

            return builder;
        }
    }
}
