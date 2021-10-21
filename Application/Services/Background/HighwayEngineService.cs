using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Background.Services
{
    public class HighwayEngineService
    {
        private readonly IServiceProvider _serviceProvider;

        public HighwayEngineService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public async Task RunEngine()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var CarsInHighway = await dbcontext.CarsInHighWay.Include(p => p.Highway).Include(p => p.Driver).AsNoTracking().ToListAsync();
                TimeSpan duration = TimeSpan.MinValue;
                double DiffrenceTime = 0;
                Double HighwayLenght = 1000;

                CarsInHighway.ForEach(async Car =>
                {
                    if (Car.Location < HighwayLenght)
                    {
                        duration = DateTime.Now - Car.LastLocationChangeTime;
                        DiffrenceTime = duration.TotalMilliseconds;
                        Car.Location += (Car.Driver.Speed * 0.00027778) * (DiffrenceTime);
                        Car.LastLocationChangeTime = DateTime.Now;
                    }
                    else
                    {
                        Car.Location = 0;
                        Car.LastLocationChangeTime = DateTime.Now;
                        if (Car.Highway.HighWayDirection == "North")
                        {
                            Car.Highway =await dbcontext.Highways.FirstOrDefaultAsync(p => p.HighWayDirection == "South");
                        }
                        else if (Car.Highway.HighWayDirection == "South")
                        {
                            Car.Highway =await dbcontext.Highways.FirstOrDefaultAsync(p => p.HighWayDirection == "North");
                        }
                    }
                   await dbcontext.SaveChangesAsync();
                });
            }
        }
    }
}