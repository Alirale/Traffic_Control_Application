using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TicketModifyResponse
    {
        public int TicketId { get; set; }
        public String PlateNumber { get; set; }
        public int TicketsListId { get; set; }
    }
}
