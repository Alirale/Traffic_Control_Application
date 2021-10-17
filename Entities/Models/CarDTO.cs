using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.NewModels
{
    public class CarDTO
    {
        public string CarName { get; set; }
        public String PlateNumber { get; set; }
        public ICollection<TicketsDTO> TicketsList { get; set; }
    }
}
