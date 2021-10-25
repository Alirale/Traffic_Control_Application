using Core.Entities.Background;
using Core.Entities.Police;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.Seeds
{
    public class DataBaseContextSeed
    {
        public static void Seeds(ModelBuilder modelBuilder)
        {
            foreach (var Role in GetRoleTypes())
            {
                modelBuilder.Entity<Role>().HasData(Role);
            }
            foreach (var Highway in GetHighwayTypes())
            {
                modelBuilder.Entity<Highway>().HasData(Highway);
            }

            foreach (var persons in GetPersonTypes())
            {
                modelBuilder.Entity<Person>().HasData(persons);
            }

            foreach (var TicketsList in GetCarsListTypes())
            {
                modelBuilder.Entity<CarsList>().HasData(TicketsList);
            }

            foreach (var TicketsList in GetCarTypes())
            {
                modelBuilder.Entity<Car>().HasData(TicketsList);
            }

            foreach (var TicketsList in GetTicketsListsTypes())
            {
                modelBuilder.Entity<TicketsList>().HasData(TicketsList);
            }

        }

        private static IEnumerable<Role> GetRoleTypes()
        {
            return new List<Role>()
            {
                new Role() {Id=1, RoleName="Citizen"},
                new Role() {Id=2, RoleName="Police"},
                new Role() {Id=3, RoleName="SuperAdmin"},
            };
        }

        private static IEnumerable<Highway> GetHighwayTypes()
        {
            return new List<Highway>()
            {
                new Highway() {Id=1 , HighWayDirection = "North", Wheather = "Sunny", MaxAllowedSpeed = 90 },
                new Highway() {Id=2 , HighWayDirection = "South", Wheather = "Sunny", MaxAllowedSpeed = 90 }
            };
        }

        private static IEnumerable<Person> GetPersonTypes()
        {
            return new List<Person>()
            {
                new Person() {Id =1, Name = "Alireza",RoleId=3  },
                new Person() {Id =2, Name = "Mohamad" ,RoleId=1 },
                new Person() {Id =3, Name = "Abbas" ,RoleId=1   },
                new Person() {Id =4, Name = "Reza" ,RoleId=1    },
            };
        }
        private static IEnumerable<CarsList> GetCarsListTypes()
        {
            return new List<CarsList>()
            {
                new CarsList() {Id = 1, Name = "Pride", CarLength = 1.6, MaxSpeed = 140,  },
                new CarsList() {Id = 2, Name = "L90", CarLength = 1.75, MaxSpeed  = 160   },
                new CarsList() {Id = 3, Name = "Sonata", CarLength = 1.8, MaxSpeed = 180  },
                new CarsList() {Id = 4, Name = "Sorento", CarLength = 1.8, MaxSpeed = 200 }
            };
        }

        private static IEnumerable<Car> GetCarTypes()
        {
            return new List<Car>()
            {
                new Car(){Id = 1, PlateNumber = "64P712",  OwnerId=1, carsListId =1, },
                new Car(){Id = 2, PlateNumber = "87G725",  OwnerId=1, carsListId =4, },
                new Car(){Id = 3, PlateNumber = "59J973",  OwnerId=2, carsListId =2, },
                new Car(){Id = 4, PlateNumber = "16T781",  OwnerId=3, carsListId =3, },
                new Car(){Id = 5, PlateNumber = "87G725",  OwnerId=4, carsListId =4, },
            };
        }

        private static IEnumerable<TicketsList> GetTicketsListsTypes()
        {
            return new List<TicketsList>()
            {
                new TicketsList() {Id=1, Name = "UnauthorizedSpeed", Price = 200000 }
            };
        }



    }
}