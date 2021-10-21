using Application.Services.Police;
using Core.Models;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundTask.Services
{
    public class CameraSpeedCheckService
    {
        private readonly IServiceProvider _serviceProvider;

        public CameraSpeedCheckService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }



        public async Task RunSpeedCheck()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var _carCrudService = scope.ServiceProvider.GetRequiredService<ITicketService>();

                var cars =await dbcontext.cars.Include(p => p.CarInHighway).ThenInclude(p => p.Driver).Include(p => p.CarInHighway).ThenInclude(p => p.Highway).AsNoTracking().ToListAsync();
                var SpeedTicket =await dbcontext.ticketsLists.FirstOrDefaultAsync(p => p.Name == "UnauthorizedSpeed");
                cars.ForEach(async Car=> {
                    if (Car.CarInHighway != null && Car.CarInHighway.Driver.Speed > Car.CarInHighway.Highway.MaxAllowedSpeed)
                    {
                        await _carCrudService.Add(new AddCarTicketDTO
                        {
                            PlateNumber = Car.PlateNumber,
                            TicketsListId = SpeedTicket.Id
                        });
                        await dbcontext.SaveChangesAsync();
                    }
                });
                
            }
        }
    }
}