using Core.Entities.Background;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Background.Services
{
    public interface IEnterCarInHighwayService
    {
        public void RunCarEntrance();
    }
    public class EnterCarInHighwayService : IEnterCarInHighwayService
    {
        private readonly IServiceProvider _serviceProvider;

        public EnterCarInHighwayService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void RunCarEntrance()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                bool AddedOneCar = false;
                var dbcontext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var PersonsCars = dbcontext.persons.Include(p => p.Cars).ThenInclude(p => p.carsList).AsNoTracking().ToList();
                ;
                PersonsCars.ForEach(Person =>
                {
                    foreach (var Car in Person.Cars)
                    {
                        if (dbcontext.CarsInHighWay.Include(p => p.Car).ToList().Any(p => p.CarId == Car.Id)) { }
                        else
                        {
                            if (AddedOneCar == false)
                            {
                                dbcontext.CarsInHighWay.Add(new CarInHighway()
                                {
                                    CarId = Car.Id,
                                    Driver = new Driver() { Speed = 60 },
                                    LastLocationChangeTime = DateTime.Now,
                                    Location = 0,
                                    HighwayId = 1
                                });
                                dbcontext.SaveChanges();
                                AddedOneCar = true;
                            }
                        }
                    }
                });
                AddedOneCar = false;
            }
        }
    }
}
