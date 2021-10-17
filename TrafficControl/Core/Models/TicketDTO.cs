using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficControl.Core.Models.NewModels
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public DateTime TicketDate { get; set; }
        public CarTicketDTO Car { get; set; }
        public TicketListDTO TicketsList { get; set; }
    }
}
