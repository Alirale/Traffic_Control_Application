using Core.Entities.Police;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class IdLessTicket
    {
        public int CarId { get; set; }
        public TicketsList TicketsList { get; set; }
        public DateTime TicketDate { get; set; }
    }
}
