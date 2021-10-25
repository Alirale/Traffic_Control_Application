using System;

namespace Core.Models
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public DateTime TicketDate { get; set; }
        public CarTicketDTO Car { get; set; }
        public TicketListDTO TicketsList { get; set; }
    }
}
