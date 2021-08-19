using Entities;
using Entities.Background;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BackgroundTask.Service
{
    public class SpeedCameraGenerator
    {
        private readonly IServiceProvider _serviceProvider;

        public SpeedCameraGenerator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }

        public void AutoGenerates(int CamCount, double HighwayLengh)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<IDatabasecontextBackground>();

                //Deletes Cars
                var Cars = dbcontext.cars.ToList();
                if (Cars.Any())
                {
                    foreach (var Car in Cars)
                    {
                        dbcontext.cars.Remove(Car);
                    }
                    dbcontext.SaveChanges();
                }

                //Deletes CarsLists
                var CarsLists = dbcontext.CarsLists.ToList();
                if (CarsLists.Any())
                {
                    foreach (var CarsList in CarsLists)
                    {
                        dbcontext.CarsLists.Remove(CarsList);
                    }
                    dbcontext.SaveChanges();
                } 

                //Deletes persons
                var persons = dbcontext.persons.ToList();
                if (persons.Any())
                {
                    foreach (var person in persons)
                    {
                        dbcontext.persons.Remove(person);
                    }
                    dbcontext.SaveChanges();
                }

                //Deletes Higways
                var Higways = dbcontext.Highways.ToList();
                if (Higways.Any())
                {
                    foreach (var highway in Higways)
                    {
                        dbcontext.Highways.Remove(highway);
                    }
                    dbcontext.SaveChanges();
                }

                //Delete Drivers
                var Drivers = dbcontext.Drivers.ToList();
                if (Drivers.Any())
                {
                    foreach (var Driver in Drivers)
                    {
                        dbcontext.Drivers.Remove(Driver);
                    }
                    dbcontext.SaveChanges();
                }


                //Generate Highways
                dbcontext.Highways.Add(new Highway() {HighWayDirection = "North", Wheather = "Sunny", MaxAllowedSpeed = 90 });
                dbcontext.Highways.Add(new Highway() {HighWayDirection = "South", Wheather = "Sunny", MaxAllowedSpeed = 90 });
                dbcontext.SaveChanges();

                //Generate Peaple
                dbcontext.persons.Add(new Person() {Name = "Alireza" });
                dbcontext.persons.Add(new Person() {Name = "Mohamad" });
                dbcontext.persons.Add(new Person() {Name = "Abbas" });
                dbcontext.persons.Add(new Person() {Name = "Reza" });
                dbcontext.SaveChanges();


                //Generate CarsList
                dbcontext.CarsLists.Add(new CarsList() {Name = "Pride" ,CarLength= 1.6 , MaxSpeed =140 });
                dbcontext.CarsLists.Add(new CarsList() {Name = "L90" ,CarLength= 1.75 , MaxSpeed =160 });
                dbcontext.CarsLists.Add(new CarsList() {Name = "Sonata" ,CarLength= 1.8 , MaxSpeed =180 });
                dbcontext.CarsLists.Add(new CarsList() {Name = "Sorento" ,CarLength= 1.8 , MaxSpeed =200 });
                dbcontext.SaveChanges();


                //Generate Cars
                dbcontext.cars.Add(new Car() {PlateNumber = "64P712" , Owner =dbcontext.persons.FirstOrDefault(p=>p.Name== "Alireza"),carsList = dbcontext.CarsLists.FirstOrDefault(p=>p.Name == "Pride") });
                dbcontext.cars.Add(new Car() {PlateNumber = "87G725", Owner =dbcontext.persons.FirstOrDefault(p=>p.Name== "Alireza"),carsList = dbcontext.CarsLists.FirstOrDefault(p=>p.Name == "Sorento") });
                dbcontext.cars.Add(new Car() {PlateNumber = "59J973", Owner =dbcontext.persons.FirstOrDefault(p=>p.Name== "Mohamad"),carsList = dbcontext.CarsLists.FirstOrDefault(p=>p.Name == "L90") });
                dbcontext.cars.Add(new Car() {PlateNumber = "16T781", Owner =dbcontext.persons.FirstOrDefault(p=>p.Name== "Abbas"),carsList = dbcontext.CarsLists.FirstOrDefault(p=>p.Name == "Sonata") });
                dbcontext.cars.Add(new Car() {PlateNumber = "87G725", Owner =dbcontext.persons.FirstOrDefault(p=>p.Name== "Reza"),carsList = dbcontext.CarsLists.FirstOrDefault(p=>p.Name == "Sorento") });
                dbcontext.SaveChanges();

                //Generate TicketList
                //Deletes TicketsList
                var TicketsLists = dbcontext.ticketsList.ToList();
                if (!TicketsLists.Any())
                {
                    dbcontext.ticketsList.Add(new TicketsList() {Name = "UnauthorizedSpeed", Price = 120000 });
                    dbcontext.SaveChanges();
                }
                


                var Cams = dbcontext.SpeedCameras;
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
                    foreach (var Loc in Lacations)
                    {
                        dbcontext.SpeedCameras.Add(new Entities.Background.SpeedCamera()
                        {
                            Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North"),
                            HighWayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North").Id,

                            Location = Loc

                        });
                    }
                    foreach (var Loc in Lacations)
                    {
                        dbcontext.SpeedCameras.Add(new Entities.Background.SpeedCamera()
                        {
                            Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "South"),
                            HighWayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "South").Id,

                            Location = Loc

                        });
                    }
                    dbcontext.SaveChanges();
                }
                else
                {
                    foreach (var Camera in Cams)
                    {
                        dbcontext.SpeedCameras.Remove(Camera);
                    }
                    dbcontext.SaveChanges();

                    foreach (var Loc in Lacations)
                    {
                        dbcontext.SpeedCameras.Add(new Entities.Background.SpeedCamera()
                        {
                            Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North"),
                            HighWayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North").Id,

                            Location = Loc

                        });
                    }
                    foreach (var Loc in Lacations)
                    {
                        dbcontext.SpeedCameras.Add(new Entities.Background.SpeedCamera()
                        {
                            Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "South"),
                            HighWayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "South").Id,

                            Location = Loc

                        });
                    }
                    dbcontext.SaveChanges();
                }

            }
        }


    }
}
