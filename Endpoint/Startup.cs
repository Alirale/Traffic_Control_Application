using Core.Models;
using Endpoint.Configs;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TrafficControl.Core.AutoMapperProfile;

namespace Endpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private TokenModel tokenSettings { get; set; }


        public void ConfigureServices(IServiceCollection services)
        {
            string conection = Configuration["conection"];
            bool RunBackgroundTask = Convert.ToBoolean(Configuration["RunBackgroundTask"].ToLower());
            tokenSettings = Configuration.GetSection("JWtConfig").Get<TokenModel>();

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DatabaseContext>(o => o.UseSqlServer(conection));

            services.AddJwtAuthorization(tokenSettings);
            services.AddTrafficControlServices(RunBackgroundTask);
            services.AddAutoMapper(typeof(MyProfile));
            services.AddControllersWithViews();
            services.AddControllers();
            services.AddSwaggerService();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwaggerService(env);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
