using NSE.Identidade.API.Interfaces;
using NSE.Identidade.API.Services;

namespace NSE.Identidade.API.Extensions
{
    public static class DependencyInjections
    {
        public static IServiceCollection DependencyInjectConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();
            return services;
        }
    }
}
