using Correlate.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace OEMS.Logger.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLogCorrelation(this IServiceCollection services)
        {
            services.AddCorrelate();

            return services;
        }
    }
}
