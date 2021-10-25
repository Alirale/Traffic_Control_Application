using Infrastructure;
using Infrastructure.EntityExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Background.Services
{
    public interface IEjectAllCarsinHighwayService
    {
        public void Eject();
    }

    public class EjectAllCarsinHighwayService : IEjectAllCarsinHighwayService
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
                var dbcontext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                dbcontext.Drivers.Clear();
                var PersonsCars = dbcontext.persons.Include(p => p.Cars).ThenInclude(p => p.carsList).Include(p => p.Cars).ThenInclude(p => p.CarInHighway).ToList();
                PersonsCars.ForEach(Person =>
                {
                    foreach (var Car in Person.Cars)
                    {
                        if (dbcontext.CarsInHighWay.Include(p => p.Car).Any(p => p.Car == Car))
                        {
                            dbcontext.CarsInHighWay.Remove(Car.CarInHighway);
                        }
                    }
                });
                dbcontext.SaveChanges();
            }
        }

    }
}