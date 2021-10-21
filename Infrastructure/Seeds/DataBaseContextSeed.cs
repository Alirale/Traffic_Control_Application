using Core.Entities.Background;
using Core.Entities.Police;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.Seeds
{
    public class DataBaseContextSeed
    {
        public static void CatalogSeed(ModelBuilder modelBuilder)
        {
            foreach (var Highway in GetHighwayTypes())
            {
                modelBuilder.Entity<Highway>().HasData(Highway);
            }

            foreach (var persons in GetPersonTypes())
            {
                modelBuilder.Entity<Person>().HasData(persons);
            }

            foreach (var TicketsList in GetTicketsListsTypes())
            {
                modelBuilder.Entity<TicketsList>().HasData(TicketsList);
            }
        }

        private static IEnumerable<Highway> GetHighwayTypes()
        {
            return new List<Highway>()
            {
                new Highway() { HighWayDirection = "North", Wheather = "Sunny", MaxAllowedSpeed = 90 },
                new Highway() { HighWayDirection = "South", Wheather = "Sunny", MaxAllowedSpeed = 90 }
            };
        }

        private static IEnumerable<Person> GetPersonTypes()
        {
            return new List<Person>()
            {
                new Person() { Name = "Alireza" },
                new Person() { Name = "Mohamad" },
                new Person() { Name = "Abbas" },
                new Person() { Name = "Reza" },
            };
        }

        private static IEnumerable<TicketsList> GetTicketsListsTypes()
        {
            return new List<TicketsList>()
            {
                new TicketsList() { Name = "UnauthorizedSpeed", Price = 120000 }
            };
        }

    }
}
