using Aplication.Services.Police;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BackgroundTask.Service
{
    public class CameraSpeedCheckService
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
                var dbcontext = scope.ServiceProvider.GetRequiredService<IDatabasecontextBackground>();
                var _carCrudService = scope.ServiceProvider.GetRequiredService<ICarCrudService>();

                var cars = dbcontext.cars.Include(p => p.CarInHighway).ThenInclude(p => p.Driver).Include(p => p.CarInHighway).ThenInclude(p => p.Highway).ToList();
                var SpeedTicket = dbcontext.ticketsList.FirstOrDefault(p => p.Name == "UnauthorizedSpeed");
                foreach (var Car in cars)
                {
                    if (Car.CarInHighway != null && Car.CarInHighway.Driver.Speed > Car.CarInHighway.Highway.MaxAllowedSpeed)
                    {
                        _carCrudService.Add(new AddCarTicket
                        {
                            PlateNumber = Car.PlateNumber,
                            TicketsListId = SpeedTicket.Id
                        });
                        dbcontext.SaveChanges();
                    }
                }
            }
        }
    }
}