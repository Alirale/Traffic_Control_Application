using Core.Entities.Background;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Background.Services
{
    public class EnterCarInHighwayService
    {
        private readonly IServiceProvider _serviceProvider;

        public EnterCarInHighwayService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task RunCarEntrance()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                bool AddedOneCar = false;
                var dbcontext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var PersonsCars =await dbcontext.persons.Include(p => p.Cars).ThenInclude(p => p.carsList).AsNoTracking().ToListAsync();

                PersonsCars.ForEach(async Person => {
                    foreach (var Car in Person.Cars)
                    {
                        if (dbcontext.CarsInHighWay.Include(p => p.Car).ToList().Any(p => p.Car == Car)) { }
                        else
                        {
                            if (AddedOneCar == false)
                            {
                                await dbcontext.CarsInHighWay.AddAsync(new CarInHighway()
                                {
                                    Car = Car,
                                    CarId = Car.Id,
                                    Driver = new Driver() { Speed = 60 },
                                    LastLocationChangeTime = DateTime.Now,
                                    Location = 0,
                                    Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North"),
                                    HighwayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North").Id
                                });
                                await dbcontext.SaveChangesAsync();
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
