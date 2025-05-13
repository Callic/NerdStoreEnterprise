using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NSE.WebAPI.Core.Identidade
{
    public static class JwtConfiguration
    {
        public static void UseJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            var appSettingsSection = configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(c =>
            {
                c.RequireHttpsMetadata = true;
                c.SaveToken = true;
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    //validar o emissor
                    ValidateIssuer = true,
                    //valida o emissor com base na assinatura
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    //chave de criptografia da assinatura
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.JwtSettings.Secret!)),
                    ValidAudience = appSettings.JwtSettings.ValidoEm,
                    ValidIssuer = appSettings.JwtSettings.Emissor
                };
            });
        }
    }
}
