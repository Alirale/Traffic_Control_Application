using Application.Services.Police;
using Core.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BackgroundTask.Services
{
    public interface ICameraSpeedCheckService
    {
        public void RunSpeedCheck();
    }

    public class CameraSpeedCheckService : ICameraSpeedCheckService
    {
        private readonly IServiceProvider _serviceProvider;

        public CameraSpeedCheckService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }



        public void RunSpeedCheck()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var _carCrudService = scope.ServiceProvider.GetRequiredService<ITicketService>();

                var cars = dbcontext.cars.Include(p => p.CarInHighway).ThenInclude(p => p.Driver).Include(p => p.CarInHighway).ThenInclude(p => p.Highway).ToList();
                var SpeedTicket = dbcontext.ticketsLists.FirstOrDefault(p => p.Name == "UnauthorizedSpeed");
                cars.ForEach(Car =>
               {
                   if (Car.CarInHighway != null && Car.CarInHighway.Driver.Speed > Car.CarInHighway.Highway.MaxAllowedSpeed)
                   {
                       _carCrudService.AddTicketBySpeedCams(new AddCarTicketDTO
                       {
                           PlateNumber = Car.PlateNumber,
                           TicketsListId = SpeedTicket.Id
                       });
                   }
               });

            }
        }
    }
}