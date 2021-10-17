using System;

namespace TrafficControl.Core.Entities.Police
{
    public class Ticket
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public int CarId { get; set; }
        public TicketsList TicketsList { get; set; }
        public DateTime TicketDate { get; set; }
    }
}
