using System;

namespace TrafficControl.Core.Models
{
    public class EditCarTicketDTO
    {
        public int TicketId { get; set; }
        public String PlateNumber { get; set; }
        public int TicketsListId { get; set; }
    }
}
