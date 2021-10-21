
using Application.Services.Police;
using Core.Interfaces.RepositoryInterfaces;
using Infrastructure;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TrafficControl.Aplication.Services.Person;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            string conection = Configuration["conection"];
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DatabaseContext>(o => o.UseSqlServer(conection));


            //services.AddTransient<IDatabasecontextPolice, DatabaseContext>();

            services.AddTransient<IGetTickets, GetTickets>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ITicketCrudService, TicketCrudService>();

            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<ITicketListRepository, TicketListRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();

            //services.AddTransient<IDatabasecontextBackground, DatabaseContext>();
            //services.AddTransient<EnterCarInHighwayService>();
            //services.AddTransient<HighwayEngineService>();
            //services.AddTransient<CameraSpeedCheckService>();
            //services.AddTransient<DriverService>();
            //services.AddTransient<SpeedCameraGenerator>();
            //services.AddTransient<EjectAllCarsinHighwayService>();
            //services.AddTransient<IHighwayService, HighWayBackgroundService>();
            //services.AddHostedService<HighWayBackgroundService>();

            services.AddAutoMapper(typeof(MyProfile));
            services.AddControllersWithViews();

            services.AddControllers();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Endpoint v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
