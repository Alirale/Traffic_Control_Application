using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BackgroundTask.Service
{
    public class EjectAllCarsinHighwayService
    {
        private readonly IServiceProvider _serviceProvider;

        public EjectAllCarsinHighwayService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }



        public void Eject()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<IDatabasecontextBackground>();
                var PersonsCars = dbcontext.persons.Include(p => p.Cars).ThenInclude(p => p.carsList).Include(p => p.Cars).ThenInclude(p => p.CarInHighway).ToList();

                foreach (var Person in PersonsCars)
                {

                    foreach (var Car in Person.Cars)
                    {
                        if (dbcontext.CarsInHighWay.Include(p => p.Car).Any(p => p.Car == Car))
                        {
                            dbcontext.CarsInHighWay.Remove(Car.CarInHighway);
                        }

                    }
                }
                dbcontext.SaveChanges();
            }
        }

    }
}