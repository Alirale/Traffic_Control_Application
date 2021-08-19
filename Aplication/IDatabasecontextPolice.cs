using Entities;
using Microsoft.EntityFrameworkCore;

namespace Aplication
{
    public interface IDatabasecontextPolice
    {
        DbSet<Car> cars { get; set; }
        DbSet<Person> persons { get; set; }
        DbSet<Ticket> tickets { get; set; }
        DbSet<TicketsList> ticketsList { get; set; }
        DbSet<CarsList> CarsLists { get; set; }
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);

    }
}
