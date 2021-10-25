using Core.Entities.Background;
using Infrastructure;
using Infrastructure.EntityExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Background.Services
{
    public interface ISpeedCameraGenerator
    {
        public void AutoGenerates();
    }

    public class SpeedCameraGenerator : ISpeedCameraGenerator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly int CamCount;
        private readonly double HighwayLengh;
        private IConfiguration _configuration;


        public SpeedCameraGenerator(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            CamCount = int.Parse(_configuration.GetSection("CamCount").Value); ;
            HighwayLengh = double.Parse(_configuration.GetSection("HighwayLengh").Value);
        }

        public void AutoGenerates()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                var Cams = dbcontext.SpeedCameras.ToList();
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
                    Lacations.ForEach(Loc =>
                   {
                       dbcontext.SpeedCameras.Add(new SpeedCamera()
                       {
                           Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North"),
                           HighWayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North").Id,
                           Location = Loc
                       });
                   });

                    Lacations.ForEach(Loc =>
                   {
                       dbcontext.SpeedCameras.Add(new SpeedCamera()
                       {
                           Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "South"),
                           HighWayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "South").Id,
                           Location = Loc
                       });
                   });
                    dbcontext.SaveChanges();
                }
                else
                {
                    dbcontext.SpeedCameras.Clear();
                    dbcontext.SaveChanges();
                    Lacations.ForEach(Loc =>
                   {
                       dbcontext.SpeedCameras.Add(new SpeedCamera()
                       {
                           Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North"),
                           HighWayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "North").Id,
                           Location = Loc
                       });
                   });

                    Lacations.ForEach(Loc =>
                   {
                       dbcontext.SpeedCameras.Add(new SpeedCamera()
                       {
                           Highway = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "South"),
                           HighWayId = dbcontext.Highways.FirstOrDefault(p => p.HighWayDirection == "South").Id,
                           Location = Loc
                       });

                   });
                    dbcontext.SaveChanges();
                }

            }
        }

        private static void DeleteTables(DatabaseContext dbcontext)
        {
            dbcontext.cars.Clear();
            dbcontext.CarsLists.Clear();
            dbcontext.persons.Clear();
            dbcontext.Highways.Clear();
            dbcontext.Drivers.Clear();
            dbcontext.SaveChanges();
        }


    }


}
