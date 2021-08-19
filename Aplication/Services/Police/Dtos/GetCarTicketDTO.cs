using Entities;
using System;
using System.Collections.Generic;

namespace Aplication.Services.Police.Dtos
{
    public class GetCarTicketDTO
    {
        public int CarId { get; set; }
        public string PlateNumber { get; set; }
        public String CarName { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
