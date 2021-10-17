using Core.Entities.Background;
using Core.Entities.Police;
using Microsoft.EntityFrameworkCore;

namespace TrafficControl.Infrastructure
{
    public static class ConfigureDb
    {
        public static void ConfigureTables(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasMany(p => p.Cars).WithOne(p => p.Owner);
            modelBuilder.Entity<TicketsList>().HasMany(p => p.Tickets).WithOne(p => p.TicketsList);
            modelBuilder.Entity<Car>().HasMany(p => p.Tickets).WithOne(p => p.Car);
            modelBuilder.Entity<Car>().HasOne(p => p.CarInHighway).WithOne(p => p.Car).HasForeignKey<CarInHighway>(p => p.CarId);

            modelBuilder.Entity<Highway>().HasMany(p => p.CarInHighWays).WithOne(p => p.Highway);
            modelBuilder.Entity<Highway>().HasMany(p => p.SpeedCameras).WithOne(p => p.Highway);
            modelBuilder.Entity<SpeedCamera>().HasMany(p => p.DetectedCars).WithOne(p => p.SpeedCamera);
        }
    }
}
