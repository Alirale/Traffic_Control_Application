using Application.Services.Background;
using Application.Services.Cars;
using Application.Services.Police;
using Background;
using Background.Services;
using BackgroundTask.Services;
using Core.Interfaces.RepositoryInterfaces;
using Infrastructure;
using Infrastructure.Repository;
using Infrastructure.SQL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
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

            services.AddTransient<IGetTickets, GetTickets>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ITicketCrudService, TicketCrudService>();
            services.AddTransient<ICarRegisterService, CarRegisterService>();

            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<ITicketListRepository, TicketListRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ICarsListRepository, CarsListRepository>();


            services.AddTransient<EjectAllCarsinHighwayService>();
            services.AddTransient<SpeedCameraGenerator>();
            services.AddTransient<HighwayEngineService>();
            services.AddTransient<DriverService>();
            services.AddTransient<CameraSpeedCheckService>();
            services.AddTransient<EnterCarInHighwayService>();
            services.AddTransient<HighWayBackgroundService>();
            services.AddTransient<RoadFacade>();
            services.AddTransient<Starter>();

            services.AddHostedService<HighWayBackgroundService>();

            services.AddAutoMapper(typeof(MyProfile));
            services.AddControllersWithViews();

            services.AddControllers();


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
