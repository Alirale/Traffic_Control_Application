using Core.Entities.Background;
using Core.Entities.Police;
using Infrastructure;
using Infrastructure.EntityExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Background.Services
{
    public class SpeedCameraGenerator
    {
        private readonly IServiceProvider _serviceProvider;

        public SpeedCameraGenerator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task AutoGenerates(int CamCount, double HighwayLengh)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                await DeleteTables(dbcontext);
                await Generates(dbcontext);

                var Cams = await dbcontext.SpeedCameras.ToListAsync();
                int DistanceBetweenEachCam = Convert.ToInt32(HighwayLengh / CamCount);

                List<int> Lacations = new List<int>();
                for (int i = 0; i < CamCount; i++)
                {
                    var Temploc = (i + 1) * DistanceBetweenEachCam;
                    if (Temploc <= HighwayLengh)
                    {
                        Lacations.Add(Temploc);
                    }
                    else if (Temploc > HighwayLengh)
                    {
                        Lacations.Add(Convert.ToInt32(HighwayLengh));
                    }
                }

                if (!Cams.Any())
                {
                    Lacations.ForEach(async Loc =>
                    {
                        await dbcontext.SpeedCameras.AddAsync(new SpeedCamera()
                        {
                            Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North"),
                            HighWayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North").Id,
                            Location = Loc
                        });
                    });

                    Lacations.ForEach(async Loc =>
                    {
                        await dbcontext.SpeedCameras.AddAsync(new SpeedCamera()
                        {
                            Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "South"),
                            HighWayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "South").Id,
                            Location = Loc
                        });
                    });
                    await dbcontext.SaveChangesAsync();
                }
                else
                {
                    //Cams.ForEach(speedcam => dbcontext.SpeedCameras.Remove(speedcam));
                    dbcontext.SpeedCameras.Clear();
                    await dbcontext.SaveChangesAsync();
                    Lacations.ForEach(async Loc =>
                    {
                        await dbcontext.SpeedCameras.AddAsync(new SpeedCamera()
                        {
                            Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North"),
                            HighWayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North").Id,
                            Location = Loc
                        });
                    });

                    Lacations.ForEach(async Loc =>
                    {
                        await dbcontext.SpeedCameras.AddAsync(new SpeedCamera()
                        {
                            Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "South"),
                            HighWayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "South").Id,
                            Location = Loc
                        });

                    });
                    await dbcontext.SaveChangesAsync();
                }

            }
        }

        private static async Task DeleteTables(DatabaseContext dbcontext)
        {
            dbcontext.cars.Clear();
            dbcontext.CarsLists.Clear();
            dbcontext.persons.Clear();
            dbcontext.Highways.Clear();
            dbcontext.Drivers.Clear();
            await dbcontext.SaveChangesAsync();
        }


        private static async Task Generates(DatabaseContext dbcontext)
        {
            //Generate Highways
            dbcontext.Highways.Add(new Highway() { HighWayDirection = "North", Wheather = "Sunny", MaxAllowedSpeed = 90 });
            dbcontext.Highways.Add(new Highway() { HighWayDirection = "South", Wheather = "Sunny", MaxAllowedSpeed = 90 });
            await dbcontext.SaveChangesAsync();

            //Generate Peaple
            dbcontext.persons.Add(new Person() { Name = "Alireza" });
            dbcontext.persons.Add(new Person() { Name = "Mohamad" });
            dbcontext.persons.Add(new Person() { Name = "Abbas" });
            dbcontext.persons.Add(new Person() { Name = "Reza" });
            await dbcontext.SaveChangesAsync();

            //Generate CarsList
            dbcontext.CarsLists.Add(new CarsList() { Name = "Pride", CarLength = 1.6, MaxSpeed = 140 });
            dbcontext.CarsLists.Add(new CarsList() { Name = "L90", CarLength = 1.75, MaxSpeed = 160 });
            dbcontext.CarsLists.Add(new CarsList() { Name = "Sonata", CarLength = 1.8, MaxSpeed = 180 });
            dbcontext.CarsLists.Add(new CarsList() { Name = "Sorento", CarLength = 1.8, MaxSpeed = 200 });
            await dbcontext.SaveChangesAsync();

            //Generate Cars
            dbcontext.cars.Add(new Car() { PlateNumber = "64P712", Owner = dbcontext.persons.FirstOrDefault(p => p.Name == "Alireza"), carsList = dbcontext.CarsLists.FirstOrDefault(p => p.Name == "Pride") });
            dbcontext.cars.Add(new Car() { PlateNumber = "87G725", Owner = dbcontext.persons.FirstOrDefault(p => p.Name == "Alireza"), carsList = dbcontext.CarsLists.FirstOrDefault(p => p.Name == "Sorento") });
            dbcontext.cars.Add(new Car() { PlateNumber = "59J973", Owner = dbcontext.persons.FirstOrDefault(p => p.Name == "Mohamad"), carsList = dbcontext.CarsLists.FirstOrDefault(p => p.Name == "L90") });
            dbcontext.cars.Add(new Car() { PlateNumber = "16T781", Owner = dbcontext.persons.FirstOrDefault(p => p.Name == "Abbas"), carsList = dbcontext.CarsLists.FirstOrDefault(p => p.Name == "Sonata") });
            dbcontext.cars.Add(new Car() { PlateNumber = "87G725", Owner = dbcontext.persons.FirstOrDefault(p => p.Name == "Reza"), carsList = dbcontext.CarsLists.FirstOrDefault(p => p.Name == "Sorento") });
            await dbcontext.SaveChangesAsync();

            //Generate TicketList
            var TicketsLists = dbcontext.ticketsLists.ToList();
            if (!TicketsLists.Any())
            {
                dbcontext.ticketsLists.Add(new TicketsList() { Name = "UnauthorizedSpeed", Price = 120000 });
                await dbcontext.SaveChangesAsync();
            }
        }
    }


}
