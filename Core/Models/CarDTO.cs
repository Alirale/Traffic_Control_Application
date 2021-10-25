using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class CarDTO
    {
        public string CarName { get; set; }
        public String PlateNumber { get; set; }
        public ICollection<TicketsDTO> TicketsList { get; set; }
    }
}
