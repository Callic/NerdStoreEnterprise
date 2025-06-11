using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Interfaces;
using Polly;
using Polly.Extensions.Http;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddScoped<IAutenticacaoService, AutenticacaoService>();

            services.AddHttpClient<ICatalogoService, CatalogoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromMilliseconds(Math.Pow(500, retryAttempt))
                    , (outcome, timespan, retryCount, Context) =>
                        {
                            Console.WriteLine($"Tentativa {retryCount} falhou. Esperando {timespan.TotalMilliseconds}ms antes de tentar novamente. Erro: {outcome.Exception?.Message}");
                        }
                    )
                ).AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(4, TimeSpan.FromSeconds(30)));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
            return services;
        }
    }
}
