using Entities;
using Entities.Background;
using Entities.Highway;
using Microsoft.EntityFrameworkCore;

namespace BackgroundTask
{
    public interface IDatabasecontextBackground
    {
        DbSet<Car> cars { get; set; }
        DbSet<Person> persons { get; set; }
        DbSet<Ticket> tickets { get; set; }
        DbSet<TicketsList> ticketsList { get; set; }
        DbSet<CarsList> CarsLists { get; set; }

        DbSet<CarInHighway> CarsInHighWay { get; set; }
        DbSet<DetectedCarBySpeedCam> DetectedCarsBySpeedCam { get; set; }
        DbSet<Highway> Highways { get; set; }
        DbSet<SpeedCamera> SpeedCameras { get; set; }
        DbSet<Driver> Drivers { get; set; }
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);

    }
}
