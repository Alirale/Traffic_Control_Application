using Core.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Endpoint.Configs
{
    public static class JwtConfiguration
    {
        public static void AddJwtAuthorization(this IServiceCollection services, TokenModel tokenSettings)
        {
            services.AddAuthentication(Options =>
            {
                Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = tokenSettings.issuer,
                    ValidAudience = tokenSettings.audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.Key)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
                configureOptions.SaveToken = true;
            });
        }
    }
}
