using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Endpoint.Configs
{
    public static class SwagerConfiguration
    {
        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Endpoint", Version = "v1" });
                var security = new OpenApiSecurityScheme
                {
                    Name = "JWT Auth",
                    Description = "Please Insert Only Your Token Here.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(security.Reference.Id, security);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    { security , new string[]{ } }
                    });
            });
        }


        public static void UseSwaggerService(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Endpoint v1"));
        }
    }
}
