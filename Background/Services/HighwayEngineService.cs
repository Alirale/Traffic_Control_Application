using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Background.Services
{
    public class HighwayEngineService
    {
        private readonly IServiceProvider _serviceProvider;

        public HighwayEngineService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public void RunEngine()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<IDatabasecontextBackground>();
                var CarsInHighway = dbcontext.CarsInHighWay.Include(p => p.Highway).Include(p => p.Driver).ToList();
                TimeSpan duration = TimeSpan.MinValue;
                double DiffrenceTime = 0;
                Double HighwayLenght = 1000;

                foreach (var Car in CarsInHighway)
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
                            Car.Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "South");
                        }
                        else if (Car.Highway.HighWayDirection == "South")
                        {
                            Car.Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North");
                        }
                    }

                    dbcontext.SaveChanges();
                }

            }
        }

    }
}