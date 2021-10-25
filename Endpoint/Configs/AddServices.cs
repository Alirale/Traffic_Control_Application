using Application.Services.Background;
using Application.Services.Cars;
using Application.Services.Police;
using Background;
using Background.Services;
using BackgroundTask.Services;
using Core.Interfaces.RepositoryInterfaces;
using Infrastructure.Repository;
using Infrastructure.SQL.Repository;
using Microsoft.Extensions.DependencyInjection;
using TrafficControl.Aplication.Services.Peaple;

namespace Endpoint.Configs
{
    public static class AddServices
    {
        public static void AddTrafficControlServices(this IServiceCollection services, bool RunBackgroundTask)
        {
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

            if (RunBackgroundTask)
            {
                services.AddHostedService<HighWayBackgroundService>();
            }

        }
    }
}
