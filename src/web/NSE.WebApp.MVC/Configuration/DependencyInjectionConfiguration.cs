using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
            return services;
        }
    }
}
