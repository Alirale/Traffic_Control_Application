using System.Collections.Generic;

namespace TrafficControl.Core.Entities.Police
{
    public class TicketsList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

    }
}
