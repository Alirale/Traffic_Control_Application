using Core.Entities.Police;
using System;
using System.Collections.Generic;

namespace TrafficControl.Core.Models
{
    public class GetCarTicketDTO
    {
        public int CarId { get; set; }
        public string PlateNumber { get; set; }
        public String CarName { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
