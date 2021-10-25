using Core.Entities.Police;
using System;

namespace Core.Models
{
    public class IdLessTicket
    {
        public int CarId { get; set; }
        public TicketsList TicketsList { get; set; }
        public DateTime TicketDate { get; set; }
    }
}
