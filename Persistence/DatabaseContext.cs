using Aplication;
using BackgroundTask;
using Entities;
using Entities.Background;
using Entities.Highway;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DatabaseContext : DbContext, IDatabasecontextPolice, IDatabasecontextBackground
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Car> cars { get; set; }
        public DbSet<Person> persons { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<TicketsList> ticketsList { get; set; }
        public DbSet<CarsList> CarsLists { get; set; }


        public DbSet<CarInHighway> CarsInHighWay { get; set; }
        public DbSet<DetectedCarBySpeedCam> DetectedCarsBySpeedCam { get; set; }
        public DbSet<Highway> Highways { get; set; }
        public DbSet<SpeedCamera> SpeedCameras { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureTables();
        }
    }
}
