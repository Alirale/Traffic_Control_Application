﻿using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Background.Services
{
    public class DriverService
    {
        private readonly IServiceProvider _serviceProvider;

        public DriverService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }

        public async Task DriverEngine()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var CarsInHighway = await dbcontext.CarsInHighWay.Include(p => p.Highway).Include(p => p.Driver).AsNoTracking().ToListAsync();
                int DriverState = new Random().Next(0, 3);

                CarsInHighway.ForEach(async Car=> {

                    int Sign = new Random().Next(0, 2);

                    if (Sign == 0) Sign = -1;
                    else if (Sign == 1) Sign = 1;
                    if (Car.Highway.Wheather == "Sunny")
                    {
                             if (DriverState == 0) Car.Driver.Speed += (Car.Driver.Speed * 0.010)* Sign;
                        else if (DriverState == 1) Car.Driver.Speed += (Car.Driver.Speed * 0.05) * Sign;
                        else if (DriverState == 2) Car.Driver.Speed += (Car.Driver.Speed * 0.02) * Sign;
                    }
                    else if (Car.Highway.Wheather == "Rainy")
                    {
                             if (DriverState == 0) Car.Driver.Speed += (Car.Driver.Speed * 0.05)  *Sign;
                        else if (DriverState == 1) Car.Driver.Speed += (Car.Driver.Speed * 0.025) *Sign;
                        else if (DriverState == 2) Car.Driver.Speed += (Car.Driver.Speed * 0.01)  *Sign;
                    }
                    await dbcontext.SaveChangesAsync();
                });

            }
        }
    }
}
