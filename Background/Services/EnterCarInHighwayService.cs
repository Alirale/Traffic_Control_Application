using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;


namespace Background.Services
{
    public class EnterCarInHighwayService
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
                var dbcontext = scope.ServiceProvider.GetRequiredService<IDatabasecontextBackground>();
                var PersonsCars = dbcontext.persons.Include(p => p.Cars).ThenInclude(p => p.carsList).ToList();


                foreach (var Person in PersonsCars)
                {

                    foreach (var Car in Person.Cars)
                    {
                        if (dbcontext.CarsInHighWay.Include(p => p.Car).Any(p => p.Car == Car)) { }
                        else
                        {
                            if (AddedOneCar == false)
                            {
                                dbcontext.CarsInHighWay.Add(new Entities.Background.CarInHighway()
                                {
                                    Car = Car,
                                    CarId = Car.Id,
                                    Driver = new Entities.Highway.Driver() { Speed = 60 },
                                    LastLocationChangeTime = DateTime.Now,
                                    Location = 0,
                                    Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North"),
                                    HighwayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North").Id

                                });
                                dbcontext.SaveChanges();
                                AddedOneCar = true;
                            }

                        }

                    }
                }
                AddedOneCar = false;
            }
        }

    }

}
